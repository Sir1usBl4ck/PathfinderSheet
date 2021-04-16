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
    class Serializer
    {
        public void SerializeSkills(ObservableCollection<Skill> skills)
        {
            var skillsPath = $@".\Data\Skills.json";
            Directory.CreateDirectory(@".\Data");
            using (StreamWriter file = File.CreateText(skillsPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file,skills);
            }

        }

        public ObservableCollection<Skill> LoadSkills()
        {
            using (StreamReader file = File.OpenText($@".\Data\Skills.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                ObservableCollection<Skill> skills =
                    (ObservableCollection<Skill>) serializer.Deserialize(file, typeof(ObservableCollection<Skill>));
                return skills;
            }
        }

    }
}
