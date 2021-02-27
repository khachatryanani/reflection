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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WarehouseManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Person
    {

        public string Name { get; set; }
        public List<Person> people { get; set; }
    }
    public partial class MainWindow : Window
    {
        public Person person1 { get; set; }
        public Dictionary<Person, int> dic { get; set; } 
        public MainWindow()
        {
            InitializeComponent();
            TestData data = new TestData();
            data.Load();
            GroupView.ItemsSource = data.Groups;
        }

    }

    public class Entry
    {
        public int Key { get; set; }
        public string Name { get; set; }
    }
    public class Group
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public IList<Group> SubGroups { get; set; }
        public IList<Entry> Entries { get; set; }
    }

    public class TestData
    {

        public IList<Group> Groups = new List<Group>();

        public void Load()
        {
            Group grp1 = new Group() { Key = 1, Name = "Group 1", SubGroups = new List<Group>(), Entries = new List<Entry>() };
            Group grp2 = new Group() { Key = 2, Name = "Group 2", SubGroups = new List<Group>(), Entries = new List<Entry>() };
            Group grp3 = new Group() { Key = 3, Name = "Group 3", SubGroups = new List<Group>(), Entries = new List<Entry>() };
            Group grp4 = new Group() { Key = 4, Name = "Group 4", SubGroups = new List<Group>(), Entries = new List<Entry>() };

            //grp1
            grp1.Entries.Add(new Entry() { Key = 1, Name = "Entry number 1" });
            grp1.Entries.Add(new Entry() { Key = 2, Name = "Entry number 2" });
            grp1.Entries.Add(new Entry() { Key = 3, Name = "Entry number 3" });

            //grp2
            grp2.Entries.Add(new Entry() { Key = 4, Name = "Entry number 4" });
            grp2.Entries.Add(new Entry() { Key = 5, Name = "Entry number 5" });
            grp2.Entries.Add(new Entry() { Key = 6, Name = "Entry number 6" });

            //grp3
            grp3.Entries.Add(new Entry() { Key = 7, Name = "Entry number 7" });
            grp3.Entries.Add(new Entry() { Key = 8, Name = "Entry number 8" });
            grp3.Entries.Add(new Entry() { Key = 9, Name = "Entry number 9" });

            //grp4
            grp4.Entries.Add(new Entry() { Key = 10, Name = "Entry number 10" });
            grp4.Entries.Add(new Entry() { Key = 11, Name = "Entry number 11" });
            grp4.Entries.Add(new Entry() { Key = 12, Name = "Entry number 12" });

            grp4.SubGroups.Add(grp1);
            grp2.SubGroups.Add(grp4);

            Groups.Add(grp1);
            Groups.Add(grp2);
            Groups.Add(grp3);
        }
    }

}
