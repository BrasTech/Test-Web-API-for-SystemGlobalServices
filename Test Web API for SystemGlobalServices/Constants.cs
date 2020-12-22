using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Web_API_for_SystemGlobalServices
{
    /// <summary>
    /// Класс с константами, которые используются в проекте
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Все URL, которые используются в проекте
        /// </summary>
        public static class URL
        {
            public const string cbr_daily = "https://www.cbr-xml-daily.ru/daily_json.js";
            //...
        }
        /// <summary>
        /// Список всех возможных ошибок в русской локализации
        /// </summary>
        public static class Errors
        {
            public const string invalid_id = "Invalid id";
            public const string invalid_params = "Invalid params";
            public const string data_not_found = "No data for your request";
            //...
        }
    }
}
