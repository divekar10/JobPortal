using Microsoft.AspNetCore.Mvc.Filters;

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
