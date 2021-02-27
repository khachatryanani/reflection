using System;
using System.Collections.Generic;
using System.Text;

namespace ZigZagElectronics.Interfaces
{
    public interface IReturnable
    {
        // Get a returnable period in days for device
        int GetReturablePeriod();
    }
}
