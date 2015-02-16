using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;

namespace DoneEvaluator.NotificationEvaluators.DB
{
    public class CustomDbConfiguration : DbConfiguration 
    {
        public CustomDbConfiguration()
        {
            SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0")); 
        }
    }
}
