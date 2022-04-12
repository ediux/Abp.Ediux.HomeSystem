using Microsoft.Extensions.Options;

using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Data;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem
{
    public class AddInsDbContextConnectionStringResolver : DefaultConnectionStringResolver
    {
        private readonly ISettingManager _settingManager;
        private static IDictionary<string, string> _connections;


        public AddInsDbContextConnectionStringResolver(
            IOptionsMonitor<AbpDbConnectionOptions> options,
            ISettingManager settingManager) : base(options)
        {
            _settingManager = settingManager;
            _connections = _connections ?? new Dictionary<string, string>();
            Log.Logger = Log.Logger ?? (ILogger)new LoggerConfiguration();
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
            bool isrunInDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
            string defaultConnectionStringInDocker = HomeSystemConsts.GetDefultConnectionStringFromOSENV();

#if DEBUG
            if (isrunInDocker)
                Log.Logger.Information($"Docker Default Connection String: {defaultConnectionStringInDocker}");

            Log.Logger.Information($"Name Resolved: {connectionStringName}.");
#endif

            if (connectionStringName == null)
            {
                if (isrunInDocker)
                {
#if DEBUG
                    Log.Logger.Information($"Connection String {connectionStringName ?? "[Null]"} Resolved: {defaultConnectionStringInDocker}.");
#endif
                    return defaultConnectionStringInDocker;
                }
                else
                {
#if DEBUG
                    Log.Logger.Information($"Connection String {connectionStringName} Resolved: {Options.ConnectionStrings.Default}.");
#endif
                    return Options.ConnectionStrings.Default;
                }

            }

            try
            {
                string connectionString = null;
                string KeyName = $"{SharedSettingsConsts.PluginsDatabaseConnectionPrefix}{connectionStringName}";

                if (_connections.ContainsKey(connectionStringName))
                {
                    connectionString = _connections[connectionStringName];

                    if (!connectionString.IsNullOrEmpty())
                    {
#if DEBUG
                        Log.Logger.Information($"Connection String {connectionStringName} Resolved: {connectionString}.");
#endif
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
#if DEBUG
                        Log.Logger.Information($"Connection String {connectionStringName} Resolved: {connectionString}.");
#endif
                        return connectionString;
                    }
                }

                if (connectionStringName == "Default")
                {
                    if (_connections.ContainsKey(connectionStringName) == false)
                    {
                        if (isrunInDocker)
                        {
                            _connections.Add(connectionStringName, defaultConnectionStringInDocker);
                        }
                        else
                        {
                            _connections.Add(connectionStringName, Options.ConnectionStrings.Default);
                        }
                    }
#if DEBUG
                    Log.Logger.Information($"Connection String {connectionStringName} Resolved: {_connections[connectionStringName]}.");
#endif
                    return _connections[connectionStringName];
                }

                if (connectionStringName.ToLowerInvariant().StartsWith("abp"))
                {
                    if (_connections.ContainsKey(connectionStringName) == false)
                    {
                        if (isrunInDocker)
                        {
                            _connections.Add(connectionStringName, defaultConnectionStringInDocker);
                        }
                        else
                        {
                            _connections.Add(connectionStringName, Options.ConnectionStrings.Default);
                        }
                    }
#if DEBUG
                    Log.Logger.Information($"Connection String {connectionStringName} Resolved: {_connections[connectionStringName]}.");
#endif
                    return _connections[connectionStringName];
                }

                if (_settingManager != null)
                {
                    connectionString = (await _settingManager.GetAllGlobalAsync())
                        .Where(w => w.Name == KeyName)
                        .Select(s => s.Value)
                        .SingleOrDefault();

                    if (!connectionString.IsNullOrEmpty())
                    {
#if DEBUG
                        Log.Logger.Information($"Connection String {connectionStringName} Resolved: {connectionString}.");
#endif
                        if (_connections.ContainsKey(connectionStringName) == false)
                        {
                            if (isrunInDocker)
                            {
                                _connections.Add(connectionStringName, connectionString);
                            }
                            else
                            {
                                _connections.Add(connectionStringName, Options.ConnectionStrings.Default);
                            }
                        }

                        return connectionString;
                    }
                }

                if (isrunInDocker)
                {
#if DEBUG
                    Log.Logger.Information($"Connection String {connectionStringName} Resolved: {defaultConnectionStringInDocker}.");
#endif
                    return defaultConnectionStringInDocker;
                }

#if DEBUG
                Log.Logger.Information($"Connection String {connectionStringName} Resolved: {Options.ConnectionStrings.Default}.");
#endif
                return Options.ConnectionStrings.Default;
            }
            catch
            {
                return null;
            }

        }
    }
}
