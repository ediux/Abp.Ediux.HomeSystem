using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Ediux.ABP.Features.SmartAdminUI.MongoDB
{
    [ConnectionStringName(SmartAdminUIDbProperties.ConnectionStringName)]
    public interface ISmartAdminUIMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
