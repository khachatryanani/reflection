using System;
using System.Collections.Generic;
using System.Text;

namespace ZigZagElectronics
{
    // Class for Refregirator type devices
    // Derives from Household base class
    public class Refregirator : Household
    {
        // Hold the info of the volume of the device in interger type
        public int Volume { get; set; }

        // Hold the info of the name of the divece's color
        public string Color { get; set; }

        // Constructor that sets the Model Type as Refregirator
        public Refregirator() : base(typeof(Refregirator).Name) 
        {

        }
    }
}
