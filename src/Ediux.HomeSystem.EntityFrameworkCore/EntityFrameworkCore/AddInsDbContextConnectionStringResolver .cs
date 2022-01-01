using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.EntityFrameworkCore
{
    public class AddInsDbContextConnectionStringResolver : DefaultConnectionStringResolver
    {
        private readonly ISettingManager _settingManager;
        private static IDictionary<string, string> _connections;
        public AddInsDbContextConnectionStringResolver(
            IOptionsSnapshot<AbpDbConnectionOptions> options,
            ISettingManager settingManager) : base(options)
        {
            _settingManager = settingManager;
            _connections= _connections ?? new Dictionary<string, string>();
        }

#pragma warning disable CS0672 // 成員會覆寫過時成員
        public override string Resolve(string connectionStringName = null)
#pragma warning restore CS0672 // 成員會覆寫過時成員
        {
            var waiter = ResolveInternalAsync(connectionStringName);
            waiter.Wait();
            return waiter.Result;
        }

        public override Task<string> ResolveAsync(string connectionStringName = null)
        {
            return ResolveInternalAsync(connectionStringName);
        }

        private async Task<string> ResolveInternalAsync(string connectionStringName)
        {
            if (connectionStringName == null)
            {
                return Options.ConnectionStrings.Default;
            }

            try
            {
                // connectionStringName.ToLowerInvariant().StartsWith("abp") || connectionStringName == "Default"

                string connectionString = null;
                string KeyName = $"ConnectionStrings_{connectionStringName}";

                if (_connections.ContainsKey(connectionStringName))
                {
                    connectionString = _connections[connectionStringName];
                    if (!connectionString.IsNullOrEmpty())
                    {
                        return connectionString;
                    }
                }

                if (Options.ConnectionStrings.ContainsKey(connectionStringName))
                {
                    connectionString = Options.ConnectionStrings[connectionStringName];

                    if (!connectionString.IsNullOrEmpty())
                    {
                        if (_connections.ContainsKey(connectionStringName) == false)
                        {
                            _connections.Add(connectionStringName, connectionString);
                        }

                        return connectionString;
                    }
                }

                if (connectionStringName == "Default")
                {
                    if (_connections.ContainsKey(connectionStringName) == false)
                    {
                        _connections.Add(connectionStringName, Options.ConnectionStrings.Default);
                    }

                    return Options.ConnectionStrings.Default;
                }

                if (connectionStringName.ToLowerInvariant().StartsWith("abp"))
                {
                    if (_connections.ContainsKey(connectionStringName) == false)
                    {
                        _connections.Add(connectionStringName, Options.ConnectionStrings.Default);
                    }

                    return Options.ConnectionStrings.Default;
                }

                //if (connectionStringName.StartsWith(HomeSystemConsts.DbTablePrefix))
                //{
                //    return Options.ConnectionStrings.Default;
                //}

                connectionString = (await _settingManager.GetAllGlobalAsync()).Where(w => w.Name == KeyName).Select(s => s.Value).SingleOrDefault();

                if (!connectionString.IsNullOrEmpty())
                {
                    if (_connections.ContainsKey(connectionStringName) == false)
                    {
                        _connections.Add(connectionStringName, Options.ConnectionStrings.Default);
                    }

                    return connectionString;
                }

                return Options.ConnectionStrings.Default;
            }
            catch
            {
                return null;
            }

        }
    }
}
