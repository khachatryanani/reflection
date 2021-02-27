using System;
using System.Collections.Generic;
using System.Text;

namespace ZigZagElectronics
{
    // Class for Tablet type devices
    // Derives from Computer base class
    public class Tablet: Computer
    {
        // Hold the info on Tablet's diaganal dimension in interger type
        public int Diaganal { get; set; }

        // Constructor that sets the Model Type as Tablet
        public Tablet() : base(typeof(Tablet).Name) 
        {
           
        }

    }
}
