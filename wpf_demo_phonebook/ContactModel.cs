using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace wpf_demo_phonebook
{
    public class ContactModel : INotifyPropertyChanged
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }


        public string Info
        {

            get => $"{FirstName}, {LastName}";

        }
                
               
            

        public ContactModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
