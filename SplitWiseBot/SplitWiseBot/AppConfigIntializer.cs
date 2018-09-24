using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace SplitWiseBot
{
    public class AppConfigIntializer
    {
        public IConfiguration config;
        public Dictionary<string, string> _globalVariables;
        
        public AppConfigIntializer()
        {
            config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            _globalVariables = new Dictionary<string, string>();
        }

        public void IntializeAppConfig()
        {
            try
            {
                #region BasicUserAddition Config
                
                #endregion

            }
            catch (Exception ex)
            {
            }
        }
    }
}
