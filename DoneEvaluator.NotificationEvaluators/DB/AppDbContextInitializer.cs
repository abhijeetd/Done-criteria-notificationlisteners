using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoneEvaluator.NotificationEvaluators.DB
{
    public class AppDbContextInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
    }
}
