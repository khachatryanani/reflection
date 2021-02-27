using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZigZagElectronics;

namespace ReflectionManager
{
    /// <summary>
    /// Classes manages the reflection on the given assembly
    /// </summary>
    public  class ProjectReflection
    {
        // Assembly object that will hold the information on dll
        private Assembly assembly = null;

        // Array of types from the assembly
        private Type[] entities = null;


        /// <summary>
        /// Parameterized constructor that initializes the assembly and entities fields
        /// </summary>
        /// <param name="assemblyName">the name of the dll to be reflected</param>
        public ProjectReflection(string assemblyName) 
        {
            try
            {
                // Load Assembly from specified dll by its name
                assembly = Assembly.Load(assemblyName);

                // Get all entities (Classes only) from the assembly into an array of Types
                entities = assembly.GetTypes().Where(x => x.IsClass).Select(x => x).ToArray();
            }
            catch (Exception)
            {
            }
        }


        /// <summary>
        /// Get the Type object from the string name of the Type
        /// </summary>
        /// <param name="typeName">name of the Type in string format</param>
        /// <returns>a Type object</returns>
        public Type GetTypeByName(string typeName)
        {
            // Iterate over all entities of the assembly and find the Type by its name
            foreach (Type type in entities)
            {
                // If the name of the current Type equals the parameter name, return the Type
                if (type.Name.Equals(typeName) || type.ToString().Equals(typeName))
                {
                    return type;
                }
            }

            // Return null if not found
            return null;
        }


        /// <summary>
        /// For the given type string name in the given assembly returns all the public protperty names in string list
        /// </summary>
        /// <param name="typeName">name of the Type in string format</param>
        /// <returns>List of strings of property names</returns>
        public List<string> GetPropertyNames(string typeName)
        {
            // Create a list of string to hold all property names in it
            List<string> propertyNames = new List<string>();

            // Get the type from the assembly by its string name
            Type type = GetTypeByName(typeName);

            // Get all the properties of the type into an array of PropertyInfos 
            PropertyInfo[] properties = type.GetProperties();

            // Iterate over the array and add the names of PropertyInfos into list of property names
            foreach (PropertyInfo prop in properties)
            {
                propertyNames.Add(prop.Name);
            }

            // Return the string list of property namse
            return propertyNames;
        }

        /// <summary>
        /// Creates an object of the Type from the given assembly
        /// </summary>
        /// <param name="typeName">type name in string format</param>
        /// <returns>an object created with Type contructor</returns>
        public object CreateInstanceOfAType(string typeName)
        {
            // Get the Type on an entitie from the assembly by its name
            Type entity = GetTypeByName(typeName);

            // Call the contructor of entity Type
            object instance = Activator.CreateInstance(entity);

            // Returnt the created instance of the Type
            return instance;
        }
        public object CreateInstanceOfAType(string typeName, List<string> properties)
        {
            // Get the Type on an entitie from the assembly by its name
            Type entity = GetTypeByName(typeName);

            // Call the contructor of entity Type
            object instance = Activator.CreateInstance(entity);

         
            SetPropertyValues(instance, typeName, properties);
            string man = (instance as Notebook).Manufacturer;
            return instance;

        }

        /// <summary>
        /// Sets the respective property values on the instance of a specifed Type of the given assembly
        /// </summary>
        /// <param name="instance">an object created of the Type contrcutor</param>
        /// <param name="typeName">the Type of the obeject's constructor</param>
        /// <param name="parameterList">values of properties to be set</param>
        /// <returns>an object created with Type contructor with all properties set</returns>
        public void SetPropertyValues(object instance, string typeName, List<string> parameterList)
        {
            // Get the Type on an entitie from the assembly by its name
            Type entity = GetTypeByName(typeName);

            // Get all the properties of the Type that should be valued
            PropertyInfo[] propertiesToSet = entity.GetProperties();

            // Zip properties to set with the parameters list with respective values
            //propertiesToSet.Zip(parameterList, (first, second) =>
            //{
            //    // Convert the value to the type property expects and set it
            //    first.SetValue(instance, Convert.ChangeType(second, first.PropertyType));
            //    return true;
            //});

            for (int i = 0; i < propertiesToSet.Length; ++i)
            {
                propertiesToSet[i].SetValue(instance, Convert.ChangeType(parameterList[i], propertiesToSet[i].PropertyType));
            }

        }


        /// <summary>
        /// Get the respective property values of the instance of a specifed Type of the given assembly
        /// </summary>
        /// <param name="instance">an object created of the Type contrcutor</param>
        /// <param name="typeName">the Type of the obeject's constructor</param>
        /// <returns>a list of instance's property values in string format</returns>
        public List<string> GetPropertyValues(object instance, string typeName)
        {
            // Create a list of string to hold the property values of the instance
            List<string> propertyValues = new List<string>();

            // Get the Type on an entitie from the assembly by its name
            Type entity = GetTypeByName(typeName);

            // Get all the properties of the Type that should be returned
            PropertyInfo[] propertiesToGet = entity.GetProperties();
            
            // Iterate over all the properties
            foreach (PropertyInfo prop in propertiesToGet)
            {
                // Get each propetie's value
                var value = prop.GetValue(instance);

                // Converrt the value to string and add to the list
                propertyValues.Add(value.ToString());
            }

            // Return the list of property values in string format
            return propertyValues;
        }


        /// <summary>
        /// From the given Assembly entities gets all the base clases 
        /// </summary>
        /// <returns>List of Types of all the Base classes from given assembly</returns>
        public List<Type> GetAllBaseClasses()
        {

            // Create a HashSet for all base classes types (use HashSet so all items are unique)
            HashSet<Type> baseTypes = new HashSet<Type>();

            // Iterate over all types in the assembly and add their base types into the hastset
            foreach (Type type in entities)
            {
                baseTypes.Add(type.BaseType);
            }

            // Convert Hashset to list and return
            return new List<Type>(baseTypes);
        }


        /// <summary>
        /// Returns the Assembly's entities base-child hierarchy in HashSet
        /// </summary>
        /// <returns></returns>
        public Dictionary<Type, List<Type>> GetAssemblyHierarchyTree()
        {
            // Create a  Dictionary to hold the base-child hierarchy
            Dictionary<Type, List<Type>> tree = new Dictionary<Type, List<Type>>();

            // Get all base classes types into HashSet
            HashSet<Type> baseClasses = new HashSet<Type>(GetAllBaseClasses());
            
            // Iterate over base classes and for each base class create a list of its child Types
            foreach (Type baseClass in baseClasses)
            {
                // Create a list of child classes of the current base class
                List<Type> childClasses = new List<Type>();

                // In the collection of all entities find the child classes of the current base class
                foreach (Type type in entities)
                {
                    if (type.BaseType == baseClass)
                    {
                        // Add the child class into the child classes list
                        childClasses.Add(type);
                    }

                    
                }
                // Add the Base class - child classes list KeyValuePair into the Hierarchy list
                tree.Add(baseClass, childClasses);
            }

            // Return the hierarchy list
            return tree;
        }

        
    }
}
