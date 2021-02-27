using System;
using System.Collections.Generic;
using System.Text;

namespace ZigZagElectronics
{
    // Class for Scanner type devices
    // Derives from Computer base class
    public class Scanner: Computer
    {
        // Hold the info whether the scanner can print in colorized mode or not
        public bool ColorScanning { get; set; }

        // Hold the info on the document paper size the Scanner can process
        public string DocumentSize { get; set; }

        // Constructor that sets the Model Type as Scanner
        public Scanner() : base(typeof(Scanner).Name) 
        {

        }

    }
}
