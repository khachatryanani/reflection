using System;
using System.Collections.Generic;
using System.Text;

namespace ZigZagElectronics
{
    // Base class of all devices of household line
    // Dervices from the main Electronics class
    public class Household: Electronics, ITransferable
    {
        // Hold the information on device's dimension
        public string Dimension { get; set; }

        // Hold the information on device's weight
        public int Weight { get; set; }

        // Parameterized constructor that initialized the model name
        public Household( string modelName): base(modelName) 
        {

        }

        // Constructor
        public Household() 
        {

        }

        // Implementation of ITransferable interface
        public double GetTransferPrice()
        {
            // Price per unit/kg for transfer
            double unitePrice = 10;

            // Total price for device
            double totalPrice = unitePrice * this.Weight;

            // Return total price
            return totalPrice;
        }
    }
}
