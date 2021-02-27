using Newtonsoft.Json;
using System;
using System.IO;

namespace SerializationManager
{
    public class JSONSerializer
    {
        public void Serialize( object obj) 
        {
            
            string elJSON = JsonConvert.SerializeObject(obj);
            File.WriteAllText("stock.json", elJSON);
        }
    }
    
}
