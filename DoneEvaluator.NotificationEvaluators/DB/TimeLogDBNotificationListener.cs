using System.Linq;

namespace DoneEvaluator.NotificationEvaluators.DB
{
    public class TimeLogDBNotificationListener : NotificationListener
    {
        public string ConnectionString { get; set; }

        public override void Notify(NotificationContext context)
        {
            var Context = context as TimeLogNotificationContext;

            if (Context != null && Context.Data != null && Context.Data.Workitems != null && Context.Data.Workitems.Count > 0)
            {
                var serviceContext = Context.Data.ServiceContext;
                if (serviceContext != null)
                {

                    using (var ctx = new AppDbContext(ConnectionString))
                    {
                        Organization org;
                        //check if organization is registered
                        if (serviceContext.Organization != null)
                        {
                            org = ctx.Organizations.Where(p => p.Title == serviceContext.Organization.Title).SingleOrDefault();
                            if (org == null)
                            {
                                org = ctx.Organizations.Add(serviceContext.Organization);
                            }
                        }
                        //Project proj;
                        //check if project is registered
                        //if (serviceContext.Project != null)
                        //{
                        //    proj = ctx.Projects.Where(p => p.Title == serviceContext.Project.Title).SingleOrDefault();
                        //    if (proj == null)
                        //    {
                        //        serviceContext.Project.Organization = org.;
                        //        proj = ctx.Projects.Add(serviceContext.Project);
                        //    }
                        //}

                        Context.Data.Workitems.ForEach(workitem =>
                        {
                            var record = ctx.TimeLogs.Where(q => q.WorkitemId == workitem.WorkitemId).SingleOrDefault();
                            if (record != null)
                            {
                                //First delete all existing observations for that date
                                ctx.Observations.RemoveRange(ctx.Observations.Where(q => q.WorkitemId == workitem.WorkitemId && q.Timestamp == workitem.TrackingDate).ToList());

                                //add workitem level observations
                                if (workitem.Observations.Count > 0)
                                {
                                    ctx.Observations.AddRange(workitem.Observations);
                                }
                                //add task level observations
                                if (workitem.Tasks.Count > 0)
                                {
                                    workitem.Tasks.ForEach(task =>
                                    {
                                        if (task.Observations.Count > 0)
                                        {
                                            ctx.Observations.AddRange(task.Observations);
                                        }
                                    });
                                }
                            }
                            else
                            {
                                ctx.TimeLogs.Add(workitem);
                            }
                        });
                        ctx.SaveChanges();
                    }
                }
            }
        }
    }
}
