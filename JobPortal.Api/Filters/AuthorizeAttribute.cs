//using JobPortal.Model;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Mvc.Filters;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Filters;
//using System.Web.Http.Results;

//namespace JobPortal.Api.Filters
//{
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
//    {
//        private readonly IList<Role> _roles;

//        public AuthorizeAttribute(params Role[] roles)
//        {
//            _roles = roles ?? new Role[] { };
//        }

//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
//            if (allowAnonymous)
//                return;

//            var user = (Role)context.HttpContext.Items["Role"];
//            if(user == null || (_roles.Any() && !_roles.Contains(user)))
//            {
//                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
//            }
//        }
//    }
//}
