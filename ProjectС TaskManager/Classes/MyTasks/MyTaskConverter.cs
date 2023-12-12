using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB_TaskManager.Classes.MyTasks
{
    public class MyTaskConverter : JsonConverter<MyTask>
    {
        public override MyTask ReadJson(JsonReader reader, Type objectType, MyTask existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            if (jObject["Title"] != null)
            {
                return jObject.ToObject<MyGeneralTask>();
            }
            else if (jObject["CourseName"] != null)
            {
                return jObject.ToObject<MyUniversityTask>();
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, MyTask value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
