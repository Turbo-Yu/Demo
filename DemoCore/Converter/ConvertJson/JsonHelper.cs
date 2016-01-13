using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;

namespace Demo.Core.Converter.ConvertJson
{
    public class JsonHelper
    {
        static readonly JsonPropertiesResolver JsonPropertiesResolver = new JsonPropertiesResolver();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = JsonPropertiesResolver };

        public static void Info<T>(T obj)
        {
            var jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented, _jsonSerializerSettings);
            _logger.Info($"\r\n{(jsonStr)}");
        }
    }
}
