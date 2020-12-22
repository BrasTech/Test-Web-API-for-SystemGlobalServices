using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Test_Web_API_for_SystemGlobalServices.Model;

namespace Test_Web_API_for_SystemGlobalServices.Controllers
{
    /// <summary>
    /// Контроллер для получения данных с сервера https://www.cbr-xml-daily.ru/
    /// </summary>
    [Route("api")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        /// <summary>
        /// Метод для получения данных о курсе валют с возможностью фильтрации по количеству
        /// и смещению
        /// </summary>
        /// <param name="count">Количество выборочных данных</param>
        /// <param name="offset">Смещение выборочных данных</param>
        /// <returns></returns>
        [HttpGet("currencies")]
        public IActionResult Get(int? count, int offset = 0)
        {
            var data = GetData();

            if (count < 0 || offset < 0) return BadRequest(new { error = Constants.Errors.invalid_params });

            if (count == null) count = data.Valute.Count;

            var filteredData = FilterData(data.Valute, (int)count, offset);

            return Ok(new { result = filteredData.Select(u=>u.Value).ToList() });
        }
        /// <summary>
        /// Метод для получения данных о курсе валют с возможностью фильтрации по идентификатору
        /// валюты
        /// </summary>
        /// <param name="id">Идентификтор валюты</param>
        /// <returns></returns>
        [HttpGet("currency/{id}")]
        public IActionResult GetById(string id)
        {
            var data = GetData();

            if (id == null) return BadRequest(new { error = Constants.Errors.invalid_params });

            var filteredData = FilterDataById(data.Valute, id);

            if (filteredData == null) return NotFound(new { error = Constants.Errors.data_not_found });

            return Ok(new { result = filteredData });
        }
        /// <summary>
        /// Метод для фильтрации данных по идентификатору.
        /// </summary>
        /// <param name="dictionary">Полученные данные о курсе валют</param>
        /// <param name="Id">Идентификатор валюты</param>
        /// <returns>Одно выборочное значение в случае успеха, NULL в случае провала.</returns>
        public IDType FilterDataById(Dictionary<string, IDType> dictionary, string Id)
        {
            return dictionary.Values.FirstOrDefault(u=>u.ID == Id);
        }
        /// <summary>
        /// Метод для фильтрации данных по количеству и смещению
        /// </summary>
        /// <param name="dictionary">Полученные данные о курсе валют</param>
        /// <param name="count">Величина выборки</param>
        /// <param name="offset">Смещение выборочныз данных</param>
        /// <returns></returns>
        public Dictionary<string, IDType> FilterData(Dictionary<string, IDType> dictionary,int count, int offset)
        {
            return dictionary.OrderBy(d => d.Key).Skip(offset).Take(count).ToDictionary(k => k.Key, v => v.Value);
        }
        /// <summary>
        /// Десериализация данных, полученных с сервера.
        /// Данные представляются в виде модели CBRModel
        /// </summary>
        /// <returns></returns>
        private CBRModel GetData()
        {
            string json = GetServerResponse();

            return JsonConvert.DeserializeObject<CBRModel>(json);
        }
        /// <summary>
        /// Получение ответа с сервера
        /// </summary>
        /// <returns></returns>
        private string GetServerResponse()
        {
            HttpClient http = new HttpClient();

            var data = http.GetAsync(Constants.URL.cbr_daily).Result.Content.ReadAsStringAsync().Result;

            return data;
        }
    }
}
