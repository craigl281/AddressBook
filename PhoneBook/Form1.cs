using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        public Contact _Contact;
        public List<Contact> _ListContacts= new List<Contact>();
        public Form1()
        {
            InitializeComponent();
            _Contact = new Contact();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Contact = (Contact)bindingSource1.Current;
            string Name = ((Contact)bindingSource1.Current).Name;
            ///First lets alpha sort our contacts list.  Or we can sort by something else if wanted
            _ListContacts.Sort((l, r) => 1 * l.Name.CompareTo(r.Name));
            ///resets datagrid
             bindingSource1.ResetBindings(false);
            ///Keeps position on contact you just saved
             bindingSource1.Position = bindingSource1.IndexOf(_Contact);       
            ///Create a Json text representation with indents for easy reading.  Of our list of objects
            var File = JsonConvert.SerializeObject(_ListContacts, Formatting.Indented);
            ///Write that text into a file
            ///I could always error handle for permissions
            System.IO.File.WriteAllText(@"C:\AddressBook.txt", File);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ///Tries to load in the addressbook file
            ///If it doesn't exist or errors out, creates a dummy 
            ///Then if it does load in to our variable, it'll plant that into our list of objects
            try
            {
                var File = System.IO.File.ReadAllText(@"C:\AddressBook.txt");
                _ListContacts = JsonConvert.DeserializeObject<List<Contact>>(File);
            }
            catch
            {
                _ListContacts.Add(new Contact("Default", "", "Palm Coast", "FL", "32164", ""));
            }

            bindingSource1.DataSource = _ListContacts;                       ///Update Binding Source with our save file
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();                    ///Knew it was only one line I needed, sometimes smart people overthink shit
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///Cancels changes
            bindingSource1[bindingSource1.Position] = _Contact;
        }

        private void bindingSource1_PositionChanged(object sender, EventArgs e)
        {
            ///Place Current Data into temp holder
            var tempholder = (Contact)bindingSource1.Current;
            ///Create temp holder incase we want to cancel
            _Contact = new Contact(tempholder.Name, tempholder.Address1, tempholder.City, tempholder.State, tempholder.Zip, tempholder.Phone);
        }

    }

    public class Contact
    {
        private string _Name;
        private string _Address1;
        private string _City;
        private string _State;
        private string _Zip;
        private string _Phone;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public Contact()
        {

        }

        public Contact(string Name, string Address1, string City, string State, string Zip, string Phone)
        {
            this.Name = Name;
            this.Address1 = Address1;
            this.City = City;
            this.State = State;
            this.Zip = Zip;
            this.Phone = Phone;
        }

    }
}
