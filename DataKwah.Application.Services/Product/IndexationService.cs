using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DataKwah.Domain.Entities;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace DataKwah.Application.Services.Product
{
    public class IndexationService : IIndexationService
    {
        public async Task IndexAsin(string asin, Domain.Entities.Product product, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(asin)) throw new ArgumentNullException(nameof(asin));

            if (product == null) product = new Domain.Entities.Product();

            HtmlDocument document;

            try
            {
                document = await LoadDocument(asin, cancellationToken);
            }
            catch (Exception)
            {
                product.ProductState.State = ProductIndexationState.Failed;
                product.ProductState.StateDate = DateTime.UtcNow;
                product.ProductState.Reason = "Page loading failed";
                return;
            }

            if (IsErrorPage(document))
            {
                product.ProductState.State = ProductIndexationState.Failed;
                product.ProductState.StateDate = DateTime.UtcNow;
                product.ProductState.Reason = "Product not found";
                return;
            }

            product.Label = GetDocumentTitle(document);

            var reviewsNode = document.DocumentNode.QuerySelector("#cm_cr-review_list");

            if (reviewsNode == default)
            {
                product.ProductState.State = ProductIndexationState.Failed;
                product.ProductState.StateDate = DateTime.UtcNow;
                product.ProductState.Reason = "No review found";
                return;
            }

            foreach (var reviewNode in reviewsNode.Descendants("div").Where(node => FilterByAttribute(node, "data-hook", "review")))
            {
                var review = product.Reviews.FirstOrDefault(r => r.Asin == reviewNode.Id);

                if (review == default)
                {
                    review = new Review
                    {
                        Asin = reviewNode.Id
                    };

                    product.Reviews.Add(review);
                }

                review.Body = GetReviewBody(reviewNode);
                review.Date = GetReviewDate(reviewNode);
                review.Rating = GetReviewRating(reviewNode);
                review.Title = GetReviewTitle(reviewNode);
            }

            product.ProductState.State = ProductIndexationState.Done;
            product.ProductState.StateDate = DateTime.UtcNow;
            product.ProductState.Reason = string.Empty;
        }

        private static async Task<HtmlDocument> LoadDocument(string asin, CancellationToken cancellationToken)
        {
            // Build URL
            var url = $"https://www.amazon.com/product-reviews/{asin}";

            // Load page as string
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Clear();
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("identity"));
            var response = await httpClient.GetStringAsync(url, cancellationToken);

            // Convert string to HtmlDocument
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(response);
            return htmlDocument;
        }

        private static bool IsErrorPage(HtmlDocument document)
        {
            var div = document.DocumentNode.QuerySelector("#h");
            return div != default;
        }

        private static string GetDocumentTitle(HtmlDocument document)
        {
            var titleNode = document.DocumentNode.Descendants("a")
                .FirstOrDefault(node => FilterByAttribute(node, "data-hook", "product-link"));

            return titleNode?.InnerText;
        }

        private static string GetReviewTitle(HtmlNode node)
        {
            var titleNode = node.Descendants("a")
                .FirstOrDefault(x => FilterByAttribute(x, "data-hook", "review-title"));

            return titleNode?.Descendants("span").FirstOrDefault()?.InnerText;
        }

        private static string GetReviewBody(HtmlNode node)
        {
            var bodyNode = node.Descendants("span")
                .FirstOrDefault(x => FilterByAttribute(x, "data-hook", "review-body"));

            return bodyNode?.Descendants("span").FirstOrDefault()?.InnerText;
        }

        private static byte? GetReviewRating(HtmlNode node)
        {
            var reviewNode = node.Descendants("i")
                .FirstOrDefault(x => FilterByAttribute(x, "data-hook", "review-star-rating"));

            var stringRating = reviewNode?.Descendants("span").FirstOrDefault()?.InnerText?.First().ToString();

            if (string.IsNullOrWhiteSpace(stringRating)) return null;

            return Convert.ToByte(stringRating);
        }

        private static DateTime? GetReviewDate(HtmlNode node)
        {
            var dateNode = node.Descendants("span")
                .FirstOrDefault(x => FilterByAttribute(x, "data-hook", "review-date"));

            var dateAndLocation = dateNode?.InnerText;

            if (string.IsNullOrWhiteSpace(dateAndLocation)) return null;

            var splitDateAndLocation = dateAndLocation.Split(" on ");

            if (splitDateAndLocation.Length < 1) return null;

            var dateString = splitDateAndLocation[1];

            if (DateTime.TryParse(dateString, out var date)) return date;

            return null;
        }

        private static bool FilterByAttribute(HtmlNode node, string attributeName, string attributeValue)
        {
            return node.Attributes.Contains(attributeName) && node.Attributes[attributeName].Value == attributeValue;
        }
    }
}
