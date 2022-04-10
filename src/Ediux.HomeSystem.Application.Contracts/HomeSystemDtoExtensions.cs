using Ediux.HomeSystem.AdditionalSystemFunctions4Users;
using Ediux.HomeSystem.SystemManagement;

using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Ediux.HomeSystem
{
    public static class HomeSystemDtoExtensions
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                /* You can add extension properties to DTOs
                 * defined in the depended modules.
                 *
                 * Example:
                 *
                 * ObjectExtensionManager.Instance
                 *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
                 *
                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Object-Extensions
                 */

                ObjectExtensionManager.Instance
                    .AddOrUpdateProperty<FileStoreDto, string>(nameof(FileStoreDto.Description), option => { option.DefaultValue = string.Empty; });
                           
                ObjectExtensionManager.Instance
                    .AddOrUpdateProperty<FileStoreDto, SMBStoreInformation>(nameof(FileStoreDto.ShareInformation), option => { option.DefaultValue = new SMBStoreInformation(); });
            });
        }
    }
}