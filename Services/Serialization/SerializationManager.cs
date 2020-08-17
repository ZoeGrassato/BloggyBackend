using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Serialization
{
    public static class SerializationManager
    {
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
