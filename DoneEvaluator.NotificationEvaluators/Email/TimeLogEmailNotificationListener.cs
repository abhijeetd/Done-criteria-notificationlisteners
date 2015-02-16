using System.Linq;

namespace DoneEvaluator.NotificationEvaluators.Email
{
    public class TimeLogEmailNotificationListener : EmailNotificationListener
    {
        public override void Notify(NotificationContext context)
        {
            var Context = context as TimeLogNotificationContext;

            if (Context != null)
            {
                var managerEmailList = Context.Data.ServiceContext.TeamProfiles.Where(p => p.Role == "Manager").Select(p => p.Email).ToList();
                var ccEmails = string.Empty;
                if (managerEmailList != null && managerEmailList.Count > 0)
                {
                    ccEmails = string.Join(",", managerEmailList);
                }

                var dataFormatter = GetPlugin<TimeLogDataFormatter>();
                if (dataFormatter != null)
                {
                    var devEmail = Context.Data.TeamMember.Email;
                    base.Notify(new EmailContext
                    {
                        To = devEmail,
                        CC = ccEmails,
                        Subject = dataFormatter.FormatTitle(Context.Data),
                        Body = dataFormatter.FormatData(Context.Data)
                    });
                }
            }
        }
    }

}
