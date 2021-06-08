﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DataKwah.Domain.Entities;
using DataKwah.Persistence.Repositories.Product;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace DataKwah.Application.Services.Product
{
    public class IndexationService : IIndexationService
    {
        public IndexationService(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        private IProductRepository ProductRepository { get; }

        public async Task IndexAsin(string asin, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(asin)) return;

            var product = new Domain.Entities.Product
            {
                Asin = asin,
                ProductState = new ProductState()
            };

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
                await ProductRepository.Add(product, cancellationToken);
                return;
            }

            if (IsErrorPage(document))
            {
                product.ProductState.State = ProductIndexationState.Failed;
                product.ProductState.StateDate = DateTime.UtcNow;
                product.ProductState.Reason = "Product not found";
                await ProductRepository.Add(product, cancellationToken);
                return;
            }

            product.Label = GetDocumentTitle(document);

            var reviewsNode = document.DocumentNode.QuerySelector("#cm_cr-review_list");

            if (reviewsNode == default)
            {
                product.ProductState.State = ProductIndexationState.Failed;
                product.ProductState.StateDate = DateTime.UtcNow;
                product.ProductState.Reason = "No review found";
                await ProductRepository.Add(product, cancellationToken);
                return;
            }

            product.Reviews = new List<Review>();

            foreach (var reviewNode in reviewsNode.Descendants("div").Where(node => FilterByAttribute(node, "data-hook", "review")))
            {
                var review = new Review
                {
                    Asin = reviewNode.Id,
                    Body = GetReviewBody(reviewNode),
                    Date = GetReviewDate(reviewNode),
                    Rating = GetReviewRating(reviewNode),
                    Title = GetReviewTitle(reviewNode)
                };

                product.Reviews.Add(review);
            }

            product.ProductState.State = ProductIndexationState.Done;
            product.ProductState.StateDate = DateTime.UtcNow;
            product.ProductState.Reason = string.Empty;
            await ProductRepository.Add(product, cancellationToken);
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
            DateTime date;

            if (DateTime.TryParse(dateString, out date)) return date;

            return null;
        }

        private static bool FilterByAttribute(HtmlNode node, string attributeName, string attributeValue)
        {
            return node.Attributes.Contains(attributeName) && node.Attributes[attributeName].Value == attributeValue;
        }
    }
}