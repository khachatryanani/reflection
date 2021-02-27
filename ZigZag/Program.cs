using System;
using System.Collections.Generic;
using System.Reflection;
using ZigZagElectronics;

namespace ZigZag
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Electronics> electronics = new List<Electronics>();

            Assembly assembly = null;
            try
            {
                assembly = Assembly.Load("ZigZagElectronics");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

           
            Type[] entities = assembly.GetTypes();

            foreach (var item in entities)
            {
                Console.WriteLine(item.Name);
            }

            //List<Type> products = new List<Type>();
            //HashSet<Type> baseTypes = new HashSet<Type>();

            //foreach (var item in entities)
            //{
            //    //Console.WriteLine(item.IsSubclassOf(typeof(System.Object)));
            //    //Console.WriteLine(item);
            //    baseTypes.Add(item.BaseType);
            //}

            //foreach (var item in entities)
            //{
            //    if (!baseTypes.Contains(item)) 
            //    {
            //        products.Add(item);
            //        //Console.WriteLine(item.FullName) ;

                    
            //    }
               

                //if (item.BaseType.ToString().Contains("Computer") || item.BaseType.ToString().Contains("Household"))
                //{
                //    Electronics elec = Activator.CreateInstance(item) as Electronics;
                //    Console.WriteLine(elec.GetType());
                //    MethodInfo method = item.GetMethod("GetModelName");
                //    Console.WriteLine(method.Invoke(elec, null));
                //    //Console.WriteLine(elec.ModelName);

                //    PropertyInfo[] propInfo = item.GetProperties();
                //    foreach (var prop in propInfo)
                //    {
                //        Console.Write($"Property {prop.Name}:");
                //        string property = Console.ReadLine();
                //        prop.SetValue(elec, Convert.ChangeType(property, prop.PropertyType));
                //        //Console.WriteLine($"Value set: {prop.GetValue(elec)}");
                //    }
                //    electronics.Add(elec);
                //}


            }
        //    foreach (var item in electronics)
        //    {
        //        PropertyInfo[] propInfo = item.GetType().GetProperties();
        //        foreach (var prop in propInfo)
        //        {

        //            Console.WriteLine($"Value set: {prop.GetValue(item)}");
        //        }
        //        //Console.WriteLine($"Value set: {prop.GetValue(elec)}");
            
        //}
        //    HashSet<Type> set = new HashSet<Type>();
        //    foreach (var item in entities)
        //    {
        //        set.Add(item.BaseType);
        //    }
        //    foreach (var item in set)
        //    {
        //        Console.WriteLine($"base type: {item}");
        //        foreach (var it in entities)
        //        {
        //            if (it.BaseType == item) 
        //            {
        //                Console.WriteLine($"derived : {it}");
        //            }
        //        }
        //    }

           
            //HashSet<KeyValuePair<Type, List<Type>>> tree = new HashSet<KeyValuePair<Type, List<Type>>>();
            //foreach (var item in baseTypes)
            //{
            //    List<Type> alltypes = new List<Type>();
            //    foreach (var derived in entities)
            //    {
            //        if (derived.BaseType == item) 
            //        {
            //            alltypes.Add(derived);
            //        }
                    
                   

            //            tree.Add(new KeyValuePair<Type, List<Type>>(item, alltypes));
                   
                    
                    
            //    }
            //}
            //foreach (var item in tree)
            //{
            //    Console.WriteLine($"base: {item.Key}");
            //    foreach (var par in item.Value)
            //    {
                    
            //        Console.WriteLine($"derived: {par}");
            //    }
            //    //Console.WriteLine(item.Value.ToString());

        //    }
        //}
    }
}
