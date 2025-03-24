using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Attributes.ExceptionFilters.Controllers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EmployeeControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        // NOTE: `OnException()`과 `OnExceptionAsync()` 모두 있는 경우, `OnExceptionAsync()` 동작 후 `OnException()`가 처리된다

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                UserNotFoundException userNotFoundException => new ObjectResult(new { userNotFoundException.Message }) { StatusCode = userNotFoundException.HttpStatusCode },
                EmployeeNotFoundException employeeNotFoundException => new ObjectResult(new { employeeNotFoundException.Message }) { StatusCode = employeeNotFoundException.HttpStatusCode },
                _ => null
            };

            context.ExceptionHandled = context.Result is not null;

            return Task.CompletedTask;
        }
    }
}
