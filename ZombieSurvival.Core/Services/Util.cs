using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.Services
{
    public static class Util
    {
        static JsonSerializerSettings settings = new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore };
        public static string SerializeAsJson<T>(T item)
        {
            return JsonConvert.SerializeObject(item);
        }

        public static T DeserializeFromJson<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input, settings);
        }
    }
}
