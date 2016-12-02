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
            _ListContacts.Sort((l, r) => 1 * l.Name.CompareTo(r.Name));
            var File = JsonConvert.SerializeObject(_ListContacts, Formatting.Indented);
            System.IO.File.WriteAllText(@"C:\AddressBook.txt", File);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            /*
            dataGridView1.DataSource = null;            ///Datagrids are gay, and you need to make datasource null
                                                        ///before you can bring in a new datasource;
                                                        ///also, why isn't it autoupdating the information like it should be?
                                                        ///the binding is already attached and should have knew it was updated
                                                        ///
            _ListContacts.Add(new Contact());           ///Creates a new contact
            bindingSource1.DataSource = _ListContacts;  ///updates this
            dataGridView1.DataSource = bindingSource1;  ///reads in the binding
            bindingSource1.MoveLast();                  ///moves to the new contact
             */
            bindingSource1.AddNew();                    ///Knew it was only one line I needed, sometimes smart people overthink shit
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
