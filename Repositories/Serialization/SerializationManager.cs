using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Serialization
{
    public static class SerializationManager
    {
        public static string Serialize<T>(T item)
        {
            return JsonConvert.SerializeObject(item);
        }
    }
}
