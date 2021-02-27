using Newtonsoft.Json;
using System;

namespace ZigZagElectronics
{
    // The base class of all electronic devices
    [Serializable]
    public class Electronics
    {
        // Holds the name of the device type
        [JsonProperty]
        private string ModelName { get;  set; }

        // Holds the name of the manufacturer
        public string Manufacturer { get; set; }

        // Parameterized constructor
        public Electronics(string modelName) 
        {
            this.ModelName = modelName;
        }

        // Constructor
        public Electronics() 
        {

        }

        // Publci method that returns the devices model name
        public string GetModelName() 
        {
            return this.ModelName;
        }
    }
}
