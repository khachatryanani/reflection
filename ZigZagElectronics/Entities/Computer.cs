using System;
using System.Collections.Generic;
using System.Text;
using ZigZagElectronics.Interfaces;

namespace ZigZagElectronics
{
    // Base class for all Computer line devices
    // Dervices from the main Electronics class
    public class Computer: Electronics, IReturnable
    {
        // Holds the information on device's operating system
        public string OS { get; set; }      

        // Parameterized constructor that initialized the model name
        public Computer(string modelName) : base(modelName)
        {

        }

        // Constructor
        public Computer() 
        {

        }

        // Implementation of IReturable interface: Computer types return period is 365 days
        public int GetReturablePeriod()
        {
            int period = 365;
            return period;
        }
    }
}
