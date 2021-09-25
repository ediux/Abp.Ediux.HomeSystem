using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Ediux.ABP.Features.SmartAdminUI.MongoDB
{
    [ConnectionStringName(SmartAdminUIDbProperties.ConnectionStringName)]
    public class SmartAdminUIMongoDbContext : AbpMongoDbContext, ISmartAdminUIMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureSmartAdminUI();
        }
    }
}