using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserInterface.Models;

namespace UserInterface.Data
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
                ObservableCollection<T> collection =
                    (ObservableCollection<T>) serializer.Deserialize(file, typeof(ObservableCollection<T>));
                return collection;
            }
        }

    }
}
