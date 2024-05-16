using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiApplication.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        //public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        //{
        //    context.Response.StatusCode = 200;
        //    using (var writer = new StreamWriter(context.Response.Body))
        //    {
        //        await writer.WriteLineAsync("Done!");
        //        return;
        //    }
        //}

        public override async void OnActionExecuting(ActionExecutingContext context) // метод выполняется до того как выполняется Action в контроллере.
                                                                                     // в context есть вся инфо о контексте запроса
        {
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
            var message = $"Executing action {actionName} on controller {controllerName}.\n";

            File.AppendAllText("D:\\Works\\IT\\RepositoryCS\\WebApplicationDev\\ApiApplication\\Filters\\log.txt", message);

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)  // метод выполняется после того как выполняется Action в контроллере
        {
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
            var message = $"Executing action {actionName} on controller {controllerName}.\n";

            File.AppendAllText("D:\\Works\\IT\\RepositoryCS\\WebApplicationDev\\ApiApplication\\Filters\\log.txt", message);

            base.OnActionExecuted(context);
        }
    }
}
