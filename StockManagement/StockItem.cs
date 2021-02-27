using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement
{
    // Class for items that hold an info on the object and other stock related properties
    [Serializable]
    public class StockItem
    {
        // Current ID for the stock item
        public int Id { get; set; }

        // Type of the stock item object which is also the class type
        public string Type { get; private set; }

        // Object added as a Stockitem
        public object Item { get; set; }

        // Parameterized Constructor
        public StockItem(int id, object item) 
        {
            this.Id = id;

            this.Type = item.GetType().Name;

            this.Item = item;
        }

        // Parameterless Constructor
        public StockItem() 
        {

        }
    }
}
