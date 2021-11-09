using Ediux.HomeSystem.Models.DTOs.SystemSettings;

using System.Threading.Tasks;

using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.SettingManagement
{
    public interface ISettingManagementAppService : ISettingManager
    {
        /// <summary>
        /// 取得全域設定值
        /// </summary>
        /// <param name="name">設定值名稱</param>
        /// <returns></returns>
        Task<string> GetGlobalOrNullAsync(string name);

        /// <summary>
        /// 設定全域設定值
        /// </summary>
        /// <param name="name">設定值名稱</param>
        /// <param name="value">設定值</param>
        /// <returns></returns>
        Task SetGlobalAsync(string name, string value);

        Task UpdateWebSettingAsync(SystemSettingsDTO input);
    }
}
