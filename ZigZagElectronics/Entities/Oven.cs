using System;
using System.Collections.Generic;
using System.Text;

namespace ZigZagElectronics
{
    // Class for Oven type devices
    // Derives from Household base class
    public class Oven: Household
    {
        // Hold the info on the Power of Volts in intergert format
        public int Power { get; set; }

        // Constructor that sets the Model Type as Oven
        public Oven() : base(typeof(Oven).Name) 
        {

        }
    }
}
