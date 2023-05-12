using System.Collections.Generic;
using System.IO;

using System;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Hospital.Model;
using Hospital.Model.DAO;




namespace Hospital.Serializer
{
    class Serializer<T> where T: new()
    {
        public List<T> FromJSON(string fileName)
        {
            var settings = new JsonSerializerSettings { DateFormatString = "dd-MM-yyyyTHH:mm:ss" };
            string jsonData = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<List<T>>(jsonData,settings);

        }

        public void ToJSON(string fileName, List<T> _data)
        {
            var settings = new JsonSerializerSettings { DateFormatString = "dd-MM-yyyyTHH:mm:ss" };
            settings.Formatting = Formatting.Indented;
            //string json = System.Text.Json.JsonSerializer.Serialize(_data);
            string json = JsonConvert.SerializeObject(_data,settings);
            File.WriteAllText(fileName, json);
        }

    }

}
        
    

