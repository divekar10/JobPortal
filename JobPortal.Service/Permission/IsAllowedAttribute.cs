using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace JobPortal.Service.Permission
{
    public class IsAllowedAttribute : ActionFilterAttribute
    {
        private string _permission;
        public IsAllowedAttribute(string permision)
        {
            this._permission = permision;
        }

        //public override void OnActionExecuting(HttpActionContext context)
        //{
            
        //}
    }
}
