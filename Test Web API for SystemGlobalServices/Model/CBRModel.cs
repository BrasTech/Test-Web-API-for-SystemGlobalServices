using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Web_API_for_SystemGlobalServices.Model
{
    /// <summary>
    /// Модель для хранения данных, полученных с сервера
    /// </summary>
    public class CBRModel
    {
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, IDType> Valute { get; set; }
    }
}
