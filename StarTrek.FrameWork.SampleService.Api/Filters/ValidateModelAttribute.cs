using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StarTrek.FrameWork.SampleService.Models;
using StarTrek.FrameWork.SampleService.Models.Exceptions;

namespace StarTrek.FrameWork.SampleService.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //context.Result = new BadRequestObjectResult(context.ModelState);

                //In order to send consistent response 
                var errorMessage = new StringBuilder();
                foreach (var state in context.ModelState)
                {
                    foreach (var modelerror in state.Value.Errors)
                    {
                        errorMessage.AppendLine(modelerror.ErrorMessage);
                    }
                }
                context.Result = new ObjectResult(new ErrorResponse(ErrorCode.ModelBindingException, errorMessage.ToString())){ StatusCode = (int)HttpStatusCode.BadRequest };
            }
        }
    }
}
