using Ediux.HomeSystem.Data;

using Microsoft.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Ediux.HomeSystem.EntityFrameworkCore
{
    public static class HomeSystemEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            HomeSystemGlobalFeatureConfigurator.Configure();
            HomeSystemModuleExtensionConfigurator.Configure();

            OneTimeRunner.Run(() =>
            {
                /* You can configure extra properties for the
                 * entities defined in the modules used by your application.
                 *
                 * This class can be used to map these extra properties to table fields in the database.
                 *
                 * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
                 * USE HomeSystemModuleExtensionConfigurator CLASS (in the Domain.Shared project)
                 * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
                 *
                 * Example: Map a property to a table field:

                     ObjectExtensionManager.Instance
                         .MapEfCoreProperty<IdentityUser, string>(
                             "MyProperty",
                             (entityBuilder, propertyBuilder) =>
                             {
                                 propertyBuilder.HasMaxLength(128);
                             }
                         );

                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
                 */

                ObjectExtensionManager.Instance
               .MapEfCoreProperty<UserPasswordStore, string>("ReservedFieldName1",
               (entityBuilder, propertyBuilder) =>
               {
                   propertyBuilder.HasMaxLength(256)
                   .IsUnicode(true);
               });

                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<UserPasswordStore, string>("ReservedFieldName2",
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(256);
                    });
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<UserPasswordStore, string>("ReservedFieldName3",
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(256);
                    });
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<UserPasswordStore, string>("ReservedFieldName4",
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(256);
                    });
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<UserPasswordStore, string>("ReservedFieldName5",
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(256);
                    });
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<UserPasswordStore, string>("ReservedField1",
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(256);
                    });
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<UserPasswordStore, string>("ReservedField2",
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(256);
                    });
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<UserPasswordStore, string>("ReservedField3",
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(256);
                    });
                ObjectExtensionManager.Instance                
                    .MapEfCoreProperty<UserPasswordStore, string>("ReservedField4",
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(256);
                    });
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<UserPasswordStore, string>("ReservedField5",
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(256);
                    });
            });
        }
    }
}
