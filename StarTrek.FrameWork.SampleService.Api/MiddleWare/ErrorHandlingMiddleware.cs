using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StarTrek.FrameWork.SampleService.Models;
using StarTrek.FrameWork.SampleService.Models.Exceptions;

namespace StarTrek.FrameWork.SampleService.Api.MiddleWare
{
    public class ErrorHandlingMiddleware
    {
        private const HttpStatusCode DefaultErrorStatusCode = HttpStatusCode.InternalServerError;
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this._next = next;
            _logger = logger;
        }

        //Put DIs in method if want SCOPED(per rq) as ctr of middleware is invoked once
        public async Task InvokeAsync(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var result = new ErrorResponse(ErrorCode.UnhandledException, e.Message);
            var code = DefaultErrorStatusCode;
            //Cast exception and accordingly set the custom ErrorCode & StatusCode
            //if (exception is MyNotFoundException) 
            //{ code = HttpStatusCode.NotFound;
            //result.ErrorCode = ErrorCode.NotImplementedException;
            //}

            context.Response.ContentType = "application/json";  //else will be text
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
