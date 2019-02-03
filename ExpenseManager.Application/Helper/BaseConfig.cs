using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Helper
{
    public static class BaseConfig
    {
        public static string GetString(string key, string defaultValue)
        {
            var configVal = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(configVal))
            {
                return configVal;
            }
            else
            {
                return defaultValue;
            }
        }

        public static int GetInt(string key, int defaultValue = -1)
        {
            var configVal = ConfigurationManager.AppSettings[key];
            var intVal = 0;
            if (!string.IsNullOrEmpty(configVal) && int.TryParse(configVal, out intVal))
            {
                return intVal;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
