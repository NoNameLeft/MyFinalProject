namespace BLTC.Web.Controllers
{
    using System.Diagnostics;

    using BLTC.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ErrorController : BaseController
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var viewModel = new ErrorViewModel() { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier };

            switch (statusCode)
            {
                case 404:
                    viewModel.ErrorMessage = "Sorry, the resource you requested cannot be found.";
                    this.logger.LogWarning($"{statusCode} Error Occured. Path = {statusCodeResult.OriginalPath} and QueryString = {statusCodeResult.OriginalQueryString}");
                    break;
            }

            return this.View("Error", viewModel);
        }

        [Route("Error")]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionDetails = this.HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var viewModel = new ErrorViewModel() { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier };
            viewModel.ErrorMessage = $"Sorry, cannot execute {exceptionDetails.Path}, because {exceptionDetails.Error.Message}";

            this.logger.LogError($"The path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");

            return this.View(viewModel);
        }
    }
}
