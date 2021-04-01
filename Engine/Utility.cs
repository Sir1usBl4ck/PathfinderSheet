using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Engine.Models;
using Engine.ViewModels;

namespace Engine
{
    public static class Utility
    {
        //public static void SaveCharacter(PathFinderViewModel.PathFinderViewModel character)
        //{
        //    using (FileStream fs = new FileStream(character.NewCharacter.Name+ ".Xml", FileMode.Create))
        //    {
        //        XmlSerializer xSer = new XmlSerializer(typeof(ViewModel.PathFinderViewModel));

        //        xSer.Serialize(fs, character);
        //    }
            
        //}

        //public static Character LoadCharacter(string name)
        //{
        //    if (File.Exists(name + ".Xml"))
        //    {
        //        using (Stream stream = File.Open(name + ".Xml", FileMode.Open))
        //        {
        //            XmlSerializer _xSer = new XmlSerializer(typeof(Character));

        //           return (Character) _xSer.Deserialize(stream);

                    
        //        }
        //    }
        //    else
        //    {
        //        //creo un nuovo oggetto dati 
        //        return new Character();
        //    }
        //}
    }
}
