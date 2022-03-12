using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public class PermissionService : IPermissionService
    {
        private readonly IUserService _userService;
        public PermissionService(IUserService userService)
        {
            _userService = userService;
        }
        //public bool Authorize(int id)
        //{
        //   if(id != null)
        //    {
                
        //    }
        //}
    }
}
