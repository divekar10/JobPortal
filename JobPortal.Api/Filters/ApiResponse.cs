using JobPortal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Api.Filters
{
    public class ApiResponse : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                return;

            if (context.Result.GetType() == typeof(FileContentResult))
                return;

            var result = context.Result;

            var responseObj = new ResponseModel
            {
                Message = string.Empty,
                StatusCode = 200,
                Errors = null
            };

            switch (result)
            {
                case OkObjectResult okResult:
                    responseObj.Data = okResult.Value;
                    break;
                case ObjectResult ObjectResult:
                    var data = (Dictionary<string, object>)(ObjectResult.Value);
                    responseObj.Message = data.ContainsKey(Constants.ResponseMessageField) ? Convert.ToString(data[Constants.ResponseMessageField]) : null;
                    responseObj.Data = data.ContainsKey(Constants.ResponseDataField) ? data[Constants.ResponseDataField] : null;
                    break;
                case JsonResult jsonResult:
                    responseObj.Data = jsonResult.Value;
                    break;
                case OkResult _:
                case EmptyResult _:
                    responseObj.Data = null;
                    break;
                default:
                    responseObj.Data = result;
                    break;
            }

            context.Result = new JsonResult(responseObj);
        }
    }
}
