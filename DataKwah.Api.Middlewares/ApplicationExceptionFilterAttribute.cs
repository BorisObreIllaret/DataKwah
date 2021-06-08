using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DataKwah.Api.Middlewares
{
    public class ApplicationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            HandleException(context);
            return base.OnExceptionAsync(context);
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            if (context.ExceptionHandled == false) base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            // TODO: Handle custom exceptions
        }
    }
}