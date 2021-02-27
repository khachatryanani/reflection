using Newtonsoft.Json;
using ReflectionManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ZigZagElectronics;

namespace StockManagement
{
    /// <summary>
    /// Reflects all entities of ZigZag Electronics stock and manages it
    /// </summary>
    public partial class MainWindow : Window
    {
        // Container of entities' base-child hierarchy
        private Dictionary<Type, List<Type>> tree = new Dictionary<Type, List<Type>>();
        
        // Stock object for the Main Application
        private Stock stock = new Stock();

        // projectReflection class object to be initialized
        public ProjectReflection zigzagReflection = null;

        // Loads the main window
        public MainWindow()
        {
            // Initialize the main window components
            InitializeComponent();

            // Get the asembly name from App.config file
            string entitiesProject = ConfigurationManager.AppSettings["entities"];

            // Initialize the ProjectReflection object
            zigzagReflection = new ProjectReflection(entitiesProject);

            // Initialize the hierarchy tree
            tree = zigzagReflection.GetAssemblyHierarchyTree();

            // Load tree root on the main window
            LoadTreeRoot(tree);

            // Bind listView control to the list of stock items in Stock
            StockListView.ItemsSource = stock.GetStockItemsFromFile(zigzagReflection);
        }

        /// <summary>
        /// Event on Treeview item selection (occures only on leave nodes of the tree)
        /// Displays all the properties of the selected Treeview Item in a Pop up window
        /// </summary>
        /// <param name="sender">Treeviewitem object</param>
        /// <param name="e">eventhandler</param>
        private void OnSelect(object sender, RoutedEventArgs e)
        {
            // Get the TreeviewItem that was selected
            TreeViewItem selectedNode = EntitiesTreeView.SelectedItem as TreeViewItem;
            
            // Grad the name of the selected node into string format
            string selectedNodeName =selectedNode.Header.ToString();

            // Create a list of strings that hold all the property names of the Type
            List<string> propertyList = zigzagReflection.GetPropertyNames(selectedNodeName);
            
            // Open a window of list of properties for the selected item
            OpenPropertySetWindow(selectedNodeName, propertyList);
            
        }

        /// <summary>
        /// OPens a window of properties to SET
        /// </summary>
        /// <param name="typeName">selected item's name</param>
        /// <param name="propertyList">list of properties of selected item to display</param>
        private void OpenPropertySetWindow(string typeName, List<string> propertyList) 
        {
            // Create a PopUp dialog window and pass the list of properties
            PropertiesPopUp popUp = new PropertiesPopUp(propertyList);

            // Show the dialog window and 
            bool? dialogResult = popUp.ShowDialog();
            if (dialogResult == true)
            {
                // Get the list of property values from the PopUp window
                List<string> propertyValuesToSet = popUp.userDefinedProperties;
                
                // Create an object of selected Type by setting the respective property values
                object device = zigzagReflection.CreateInstanceOfAType(typeName, propertyValuesToSet);

                // Add to the main Stock
                stock.AddItem(device);
                UpdateLiveView();
            }

        }

        // Updates the ListView by reading JSON from File
        private void UpdateLiveView() 
        {
            StockListView.ItemsSource = stock.GetStockItemsFromFile(zigzagReflection);
        }


        /// <summary>
        /// Recursively add the child nodes of the tree hierarchy as TreeviewItems in main Treeview object of the window
        /// </summary>
        /// <param name="tree">hierarchy tree</param>
        /// <param name="item">current item of Treeview for which child nodes should be loaded</param>
        private void LoadChildNodesFromTreeToUI(Dictionary<Type, List<Type>> tree, TreeViewItem item)
        {
            // Create variable to check if the current treeview item can be a leave of the tree
            bool isLeafNode = true;

            // Iterate over the hierarchy tree
            foreach (var node in tree)
            {
                // Grad each nodes name
                string nodeName = node.Key.Name;

                // if the name (header) of the current Treeview item is equal to the node name in hierarchy tree 
                //than we can load its child nodes from the list( node.Value)
                if (item.Header.Equals(nodeName))
                {
                    // Iterate over the list of child nodes and create new TreeviewItems 
                    foreach (Type child in node.Value)
                    {
                        // Create new treeview item for the current child 
                        TreeViewItem childItem = new TreeViewItem();

                        // Add child name as a header for TreeViewItem
                        childItem.Header = child.Name;

                        // Make a recursive call to load the child nodes of the current child
                        LoadChildNodesFromTreeToUI(tree, childItem);

                        // Add the new TreeviewItem to the current TreeviewItem
                        item.Items.Add(childItem);
                    }

                    // If the current TreeViewItem name was found in the hierarchy tree, 
                    //it was a base type class so it should have child classes and cannot be a leave node
                    isLeafNode = false;
                }
            }

            // if the node was a leave node it should handle the OnSelect event
            if (isLeafNode)
            {
                item.Selected += OnSelect;
            }
        }


        /// <summary>
        /// Gets the root(s) of tree hierarchy and loads on the main window
        /// </summary>
        /// <param name="tree">base-child hierarchy of the assembly</param>
        private void LoadTreeRoot( Dictionary<Type, List<Type>> tree) 
        {
            // Iterate over the HashSet
            foreach (var node in tree)
            {
                // If the base type of any Type in the Hashset is System.Object, it means it can be the root of the tree
                if (node.Key.Equals(typeof(System.Object)))
                {
                    // Iterate over child lists of the root
                    foreach (Type type in node.Value)
                    {
                        // Create new TreeViewItem for the children Types
                        TreeViewItem treeItem = new TreeViewItem();

                        // Add the Type name as a header for the current TreeviewItem
                        treeItem.Header = type.Name;
                        
                        // Load the child nodes of the current TreeviewItem
                        LoadChildNodesFromTreeToUI(tree, treeItem);

                        // Add the TreeviewItem objects to the main TreeView object of the window
                        EntitiesTreeView.Items.Add(treeItem);
                    }
                }
            }
        }
    }
}
