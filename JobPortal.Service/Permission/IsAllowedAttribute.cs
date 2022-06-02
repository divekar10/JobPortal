using JobPortal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace JobPortal.Service.Permission
{
    public class IsAllowedAttribute : Attribute, IActionFilter
    {
        private string _permission;
        public IsAllowedAttribute(string permision)
        {
            _permission = permision;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var _permissionservice = context.HttpContext.RequestServices.GetService(typeof(IPermissionService)) as IPermissionService;
            string[] permissions = _permission.Split(',');
             
            bool isAuthorize = false;

            foreach (var permission in permissions)
            {
                isAuthorize = _permissionservice.Authorize(permission.Trim());

                if (isAuthorize)
                    break;
            }

            if (!isAuthorize)
            {
                context.Result = new BadRequestObjectResult(new Response { Code = 403, Message = "Access denied" });
            }
        }
    }
}
