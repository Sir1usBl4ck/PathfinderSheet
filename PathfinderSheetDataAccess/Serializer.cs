using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace PathfinderSheetDataAccess
{ 
    public class Serializer
    {
        public void SerializeCollection<T>(ObservableCollection<T> t,string filename)
        {
           
            var filePath = $@".\Data\{filename}.json";
            Directory.CreateDirectory(@".\Data");
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file,t);
            }

        }

        public ObservableCollection<T> LoadCollection<T>(string filename)
        {
            using (StreamReader file = File.OpenText($@".\Data\{filename}.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                ObservableCollection<T> collection =
                    (ObservableCollection<T>) serializer.Deserialize(file, typeof(ObservableCollection<T>));
                return collection;
            }
        }

    }
}
