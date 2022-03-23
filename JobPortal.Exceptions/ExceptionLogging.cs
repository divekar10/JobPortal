using JobPortal.Database;
using JobPortal.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Exceptions
{
    public class ExceptionLogging
    {
       public static void LogExceptionToDb(Exception ex)
        {
            using(var db = new JobDbContext())
            {
                string message = "";
                if (ex.InnerException != null)
                {
                    message = ex.InnerException.ToString();
                }

                string url = "New";
                //if (HttpContext. != null)
                //    url = HttpContext.Current.Request.Url.ToString();
                ExceptionLog l = new ExceptionLog();
                l.Msg = ex.Message.ToString() + message;
                l.Type = ex.GetType().Name.ToString();
                l.Url = url;
                l.Source = ex.StackTrace.ToString();
                l.CreateDate = DateTime.Now;

                db.ExceptionLog.Add(l);
                db.SaveChanges();
            }
        }
    }
}
