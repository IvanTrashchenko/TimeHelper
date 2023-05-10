using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TimeHelper.Data.DbAccess.Util
{
    public static class ConnectionString
    {
        private static string s_SqlConnStringError = null;
        private static string s_SqlConnString = GetConnectionStringInitial();

        private static string GetConnectionStringInitial()
        {
            try
            {
                const string connectionStringName = "TimeHelperDb";
                const string connectionStringEnvVarName = "SQLCONNSTR_" + connectionStringName;

                var configurationBuilder = new ConfigurationBuilder();

                IConfiguration configuration = null;

                // Environoment-specific appsettings.json (as in direct injection configuration)
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var infix = String.Empty;
                if (!String.IsNullOrEmpty(env))
                {
                    infix = $".{env}";
                }
                configuration = configurationBuilder
                    .AddJsonFile(
                        $"appsettings{infix}.json",
                        optional: true,
                        reloadOnChange: true).Build();

                string connectionString = Environment.GetEnvironmentVariable(connectionStringEnvVarName)
                                          ?? configuration[$"ConnectionStrings:{connectionStringName}"];

                if (null == connectionString)
                {
                    s_SqlConnStringError = $"No ConnectionString found for '{connectionStringName}'.";
                }
                return connectionString;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string Get()
        {
            if (null != s_SqlConnStringError)
                throw new ApplicationException(s_SqlConnStringError);

            return s_SqlConnString;
        }
    }
}
