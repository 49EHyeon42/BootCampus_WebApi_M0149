﻿using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Attributes.ExceptionFilters.Controllers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SignControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                UserAlreadySignInException userAlreadySignInException => new ObjectResult(new { userAlreadySignInException.Message }) { StatusCode = userAlreadySignInException.HttpStatusCode },
                UserNotFoundException userNotFoundException => new ObjectResult(new { userNotFoundException.Message }) { StatusCode = userNotFoundException.HttpStatusCode },
                _ => null
            };

            context.ExceptionHandled = context.Result is not null;
        }
    }
}
