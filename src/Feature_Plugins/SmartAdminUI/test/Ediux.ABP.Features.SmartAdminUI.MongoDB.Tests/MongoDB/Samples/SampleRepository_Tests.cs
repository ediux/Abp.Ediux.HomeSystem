using Ediux.ABP.Features.SmartAdminUI.Samples;
using Xunit;

namespace Ediux.ABP.Features.SmartAdminUI.MongoDB.Samples
{
    [Collection(MongoTestCollection.Name)]
    public class SampleRepository_Tests : SampleRepository_Tests<SmartAdminUIMongoDbTestModule>
    {
        /* Don't write custom repository tests here, instead write to
         * the base class.
         * One exception can be some specific tests related to MongoDB.
         */
    }
}
