
using Ediux.HomeSystem.Models.DTOs.FCM;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.SettingManagement
{
    public interface ISettingManagementAppService : ISettingsManagementSuperAppService, ISettingManager
    {
        Task UpdateFCMSettingsAsync(FCMSettingsDTO input);
    }
}
