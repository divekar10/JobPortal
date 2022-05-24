using JobPortal.Database.Infra;
using JobPortal.Model.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public interface IEmailTemplateRepository : IRepository<EmailTemplate>
    {
        EmailTemplate GetEmailbody(string type);
    }
    public class EmailTemplateRepository : Repository<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(JobDbContext jobDbContext) : base(jobDbContext)
        {

        }

        public EmailTemplate GetEmailbody(string type)
        {
            var emailBody = (from j in JobDbContext.EmailTemplate
                             where j.Type == type
                             select j);
            return emailBody.FirstOrDefault();
        }
    }
}
