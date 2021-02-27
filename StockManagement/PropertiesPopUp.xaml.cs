using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockManagement
{
    /// <summary>
    /// Interaction logic for PropertiesPopUp.xaml
    /// </summary>
    public partial class PropertiesPopUp : Window
    {
        public List<string> userDefinedProperties = new List<string>();
        /// <summary>
        /// Pop up window that displays all the properties of the selected item in main window
        /// and resposective textboxes for user to enter values
        /// </summary>
        /// <param name="list">list of property names in string format for the selected item in main window</param>
        public PropertiesPopUp(List<string> list)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Initialize the basic componenets of the pop up window
            InitializeComponent();
           

            // Define a default top margin to change based on the count of the list items
            double marginTop = PropertyLabel.Margin.Top + PropertyLabel.Height + 5;
       
            // for each property name in the list display a label and textbox
            foreach (string name in list)
            {
                // Create a label control and sent the content as the property name
                Label label = new Label();
                label.Content = name;

                // Set the other properties of Label control
                label.Margin = new Thickness(50, marginTop, 0, 0);
                label.VerticalAlignment = VerticalAlignment.Top;
                label.Height = 30;

                // Create a Textbox control for each item of the list
                TextBox textbox = new TextBox();

                // Set the other properties of Textbox control
                textbox.Margin = new Thickness(200, marginTop, 0, 0);
                textbox.VerticalAlignment = VerticalAlignment.Top;
                textbox.HorizontalAlignment = HorizontalAlignment.Left;
                textbox.Height = 30;
                textbox.Width = 100;

                // Add the Label and Textbox to the maingrid of the popup window
                PopUpGrid.Children.Add(label);
                PopUpGrid.Children.Add(textbox);

                // Change the Top margin by 35 points so the next controls won't overlap 
                marginTop += (label.Height + 5);

            }

            // Create a submit button after the whole lsit is displayed
            Button submit = new Button();

            // Set the other properties for Button control
            submit.Margin = new Thickness(200, marginTop, 0, 0);
            submit.Content = "Submit";
            submit.VerticalAlignment = VerticalAlignment.Top;
            submit.HorizontalAlignment = HorizontalAlignment.Left;
            submit.Height = 30;
            submit.Width = 100;
            submit.Click += SubmitButton_Click;

            // Add the Button contorl to the main grid of the popup window
            PopUpGrid.Children.Add(submit);

        }

        // Close the window on click : TO BE COMPLETED
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            

            foreach (Control control in PopUpGrid.Children)
            {
                if (control is TextBox) 
                {
                    string property = (control as TextBox).Text;
                    userDefinedProperties.Add(property);
                    
                }
            }

            
            // If Submit button is clicked, change the Dialog Result property to true
            this.DialogResult = true;
            
            Close();
        
        }

       
    }
}
