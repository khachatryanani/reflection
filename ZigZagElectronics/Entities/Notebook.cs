using System;
using System.Collections.Generic;
using System.Text;

namespace ZigZagElectronics
{
    // Class for Notebook type devices
    // Derives from Computer base class
    public class Notebook : Computer
    {
        // Hold the info on Notebook resolution in integer format
        public int Resolution { get; set; }

        // Hold the info of whether its a Touchscreen or not
        public bool Touchscreen { get; set; }

        // Constructor that sets the Model Type as Notebook
        public Notebook( ) : base(typeof(Notebook).Name) 
        {

        }
    }
}
