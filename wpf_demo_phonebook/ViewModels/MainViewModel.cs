using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using wpf_demo_phonebook.ViewModels.Commands;

namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ObservableCollection<ContactModel> contacts;
        private ContactModel selectedContact;

        public ContactModel SelectedContact
        {
            get => selectedContact;
            set { 
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ContactModel> Contacts
        {
            get => contacts;
            private set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        private string criteria;

        public string Criteria
        {
            get { return criteria; }
            set { 
                criteria = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchContactCommand { get; set; }
        public RelayCommand SaveContactCommand { get; set; }

        public RelayCommand DeleteContactCommand { get; set; }

        public MainViewModel()
        {

            SearchContactCommand = new RelayCommand(SearchContact);
            DeleteContactCommand = new RelayCommand(DeleteContact);
            SelectedContact = PhoneBookBusiness.GetContactByID(1);
            GetAllContactsFromDataBase(); //Init Value sur les autres travaille
        }

        private void GetAllContactsFromDataBase()
        {
            Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetAllContacts());
        }


        private void SearchContact(object parameter)
        {
            string input = parameter as string;
            int output;
            string searchMethod;
            if (!Int32.TryParse(input, out output))
            {
                searchMethod = "name";
            } else
            {
                searchMethod = "id";
            }

            switch (searchMethod)
            {
                case "id":
                    SelectedContact = PhoneBookBusiness.GetContactByID(output);
                    break;
                case "name":
                    SelectedContact = PhoneBookBusiness.GetContactByName(input);
                    break;
                default:
                    MessageBox.Show("Unkonwn search method");
                    break;
            }
        }

        private void DeleteContact(object parameter)
        {
            string input = parameter as string;

           if( MessageBox.Show("Etes-vous sur de supprimer l'utisateur avec ID :  " + input, "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
            int output;
            Int32.TryParse(input, out output);


            Debug.WriteLine("Tentative de supression ID  : " + output);
            

            PhoneBookBusiness.DeleteContact(output);

            //Permet de rafraichir la listeView une fois que la supression est faite 
            //#JeSuisParesseux
            GetAllContactsFromDataBase();
           


            }
                        
        }
    }
}
