using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace JobPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {

        }

        protected int UserId => int.Parse(this.User.Claims.First(x => x.Type == "UserId").Value);
        protected int RoleId => int.Parse(this.User.Claims.First(x => x.Type == "RoleId").Value);

        protected object NSModelState<T>(ModelStateDictionary model)
        {
            List<dynamic> dynamics = new();
            var list = model.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
            int i = 1;
            foreach (var item in list)
            {
                for (var k = 0; k < item.Count; k++)
                {
                    dynamics.Add(string.Concat(i++, ") ", item[k].ErrorMessage));
                }
            }
            return new { Status = typeof(T).Name + "Validation Failed", Code = 400, ReposponseData = dynamics };
        }
    }
}
