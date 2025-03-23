using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Attributes.ExceptionFilters.Controllers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class WorkExperienceControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                UserNotFoundException userNotFoundException => new ObjectResult(new { userNotFoundException.Message }) { StatusCode = userNotFoundException.HttpStatusCode },
                EmployeeNotFoundException employeeNotFoundException => new ObjectResult(new { employeeNotFoundException.Message }) { StatusCode = employeeNotFoundException.HttpStatusCode },
                WorkExperienceNotFoundException workExperienceNotFoundException => new ObjectResult(new { workExperienceNotFoundException.Message }) { StatusCode = workExperienceNotFoundException.HttpStatusCode },
                _ => null
            };

            context.ExceptionHandled = context.Result is not null;
        }
    }
}
