using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReflectionManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigZagElectronics;

namespace StockManagement
{
    /// <summary>
    /// Class for holding the Stock information and all stock items
    /// </summary>
    public class Stock
    {
        // Collection of stock items and StockItem objects
        private JArray jsonStockItems = new JArray();
        
        // Static int as an Id to be assigned to each stockItem in the collection (will start from 100)
        private static int Id = default;

        // Parameterless constructor that creates a json File
        public Stock() 
        {
            // Check if the file already exists
            if (File.Exists("stock.json"))
            {
                // If the File exisit open the file and read it to string
                string json = File.ReadAllText("stock.json");

                // Parse the json string to JArray
                jsonStockItems = JArray.Parse(json);

                // Set the static ID to continue the numeration
                Id = 100 + jsonStockItems.Count;
            }
            else 
            {
                // If File does not exisit
                // Serialize the JArray to string
                string json = JsonConvert.SerializeObject(jsonStockItems);

                // Wrtie the string in the File
                File.WriteAllText("stock.json", json);

                // Set the static ID to 100 (starting point)
                Id = 100;
            }
            
        }


        /// <summary>
        /// Creates a StockItem from the object added to the stock and Arichives in File by serializing to JSON
        /// </summary>
        /// <param name="obj">object type object added to the stock</param>
        public void AddItem(object obj) 
        {
            // Create a StockItem object
            StockItem item = new StockItem(Id, obj);

           // Increment the static Id
            ++Id;

            // Convert StockItem into JToken to add it to the json array
            JToken jsonItem = JToken.FromObject(item);
            jsonStockItems.Add(jsonItem);

            // Serialize the json Array to string
            string json = JsonConvert.SerializeObject(jsonStockItems);

            // Overwrite the File with json string
            File.WriteAllText("stock.json", json);

        }


        /// <summary>
        /// Reads the json File and creats a list of StockItems
        /// </summary>
        /// <param name="reflection">ProjectReflection object should be passed to get deserialize the JSON to appropriate types</param>
        /// <returns>List of StockItem objects</returns>
        public List<StockItem> GetStockItemsFromFile(ProjectReflection reflection) 
        {
            // Create a list of StockItems
            List<StockItem> stockItems = new List<StockItem>();

            // Read json from File
            string json = File.ReadAllText("stock.json");

            // Convert json string to JArray
            JArray jsonStock = JArray.Parse(json);
            
            // Iterate over each json item in JArray
            foreach (var jtoken in jsonStock)
            {
                // Get the current StockItem ID
                int Id = Convert.ToInt32(jtoken["Id"]);

                // Get the current object's type name from Type property
                string typeName = jtoken["Type"].ToString();

                // Get the Type by passing the typename string to Reflection object
                Type type = reflection.GetTypeByName(typeName);

                // Get the json string for Item property
                string jsonItemString = jtoken["Item"].ToString();

                // Deserialize the Item to its actualy Type
                object currentObject = JsonConvert.DeserializeObject(jsonItemString, type);

                // Create a new StockItem with ID and object
                StockItem currentStockItem = new StockItem(Id, currentObject);

                // Add to StockItems' list
                stockItems.Add(currentStockItem);
            }

            // Return the StockItems' list
            return stockItems;
        }
    }
}
