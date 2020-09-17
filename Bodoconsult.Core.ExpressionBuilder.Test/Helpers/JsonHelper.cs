using Newtonsoft.Json;

namespace Bodoconsult.Core.ExpressionBuilder.Test.Helpers
{

    /// <summary>
    /// Class with JSON helper functions
    /// </summary>
    public static class JsonHelper
    {

        /// <summary>
        /// Serialize a object to JSON
        /// </summary>
        /// <param name="data">object to serialize</param>
        /// <returns>JSON string</returns>
        public static string JsonSerialize(object data)
        {
#pragma warning disable CA2326
            var jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var jsonStr = JsonConvert.SerializeObject(data, jsonSerializerSettings);
            return jsonStr;
#pragma warning disable CA2326
        }

        /// <summary>
        /// Deserialize a JSON string to a object of type T
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="json">JSON input string</param>
        /// <returns>Deserialized data object</returns>
        public static T JsonDeserialize<T>(string json)
        {
#pragma warning disable CA2326, CA2327
            var jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var x = (T)JsonConvert.DeserializeObject(json, jsonSerializerSettings);
            return x;
#pragma warning disable CA2326, CA2327
        }

        /// <summary>
        /// Serialize a object to a nicely readable JSON string
        /// </summary>
        /// <param name="data">object to serialize</param>
        /// <returns>JSON string</returns>
        public static string JsonSerializeNice(object data)
        {
#pragma warning disable CA2326
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            var jsonStr = JsonConvert.SerializeObject(data, jsonSerializerSettings);
            return jsonStr;
#pragma warning disable CA2326
        }
    }
}
