using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECP_B2B_API_SDK.Helper
{
    /// <summary>
    /// 浮点型转换 
    /// </summary>
    public class DecimalDoubleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal)|| objectType == typeof(double)|| objectType == typeof(float)|| objectType == typeof(decimal?) || objectType == typeof(double?) || objectType == typeof(float?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //去.0
            return string.Format("{0:#0.####}", reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //去.0 
            writer.WriteValue(string.Format("{0:#0.####}", value));
        }  
    } 
}
