using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Practice1._1.Models;
using Practice1._1.Tools;
using Practice1._1.Tools.MyExceptions;
using System.Runtime.CompilerServices;
using System.Windows;
using Practice1._1.Services;
using System.Collections.ObjectModel;

namespace Practice1._1.ViewModels
{
    class PersonControlViewModel : INotifyPropertyChanged
    {        
        #region Fields
        private Window window = Application.Current.MainWindow;
        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime _bDate = DateTime.MinValue;

        private ObservableCollection<Person> _users;
        private RelayCommand<object> _checkCommand;
        private PersonService personService = new PersonService();

        private string _fName = "FName";
        private string _sName = "SName";
        private string _email = "email@gmail.com";
        #endregion

        #region Constructors
        public PersonControlViewModel()
        {
            _users = new ObservableCollection<Person>(personService.GetAllUsers());
        }
        #endregion

        #region ValueDataType
        private enum ChineseDataEnum { Monkey, Rooster, Dog, Pig, Rat, Ox, Tiger, Rabbit, Dragon, Snake, Horse, Goat };
        #endregion


        #region Properties

       
        public RelayCommand<object> AddCommand
        {
            get
            {
                return _checkCommand ?? (_checkCommand = new RelayCommand<object>(_ =>  Action(), CanExecute));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public DateTime BDate
        {
            get{
                if (_bDate == DateTime.MinValue)
                    return DateTime.Now;
                return _bDate;
            }
            set { _bDate = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Person> Users
        {
            get { return _users; }
            private set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        public string TbSName
        {
            get { return _sName; }
            set { _sName = value; OnPropertyChanged(); }
        }


        public string TbFName
        {
            get { return _fName; }
            set { _fName = value; OnPropertyChanged(); }
        }

        public string TbEmail
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        #endregion


        #region BusinessLogic
        private bool CanExecute(object obj)
        {

            if (!TbFName.Equals("") && !TbSName.Equals("") && !TbEmail.Equals(""))
                return true;
            return false;
        }

        private async Task Action()
        {
            
            try
            {
                window.IsEnabled = false;
                ІsValidBDate();
                bool isBirthday = false;
                bool isAdult = false;
                string chineseData = "";
                string westData = "";

                if (DateTime.Now.Month == BDate.Month && DateTime.Now.Day == BDate.Day)
                {
                    isBirthday = true;
                    MessageBox.Show("Happy B-Day!!!");
                }

                isAdult = AgeValue() >= 18;
                await Task.Run(() => westData = WestDataSign());
                await Task.Run(() => chineseData = ChineseDataSign());
                DBPerson _dbperson = new DBPerson(_fName, _sName, _email, BDate, isAdult, chineseData, westData, isBirthday);
                await Task.Run(() => personService.AddOrUpdateAsync(_dbperson));
                updateTable();
                return;
            }                            
            catch (InvalidPersonDataException ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catch  (InvalidDateException ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            finally{
                СlearFields();
                window.IsEnabled = true;
            }
        }

        private void updateTable()
        {
            Users = new ObservableCollection<Person>(personService.GetAllUsers());
        }

        private void СlearFields()
        {
            TbFName = "";
            TbSName = "";
            TbEmail = "";
            BDate = DateTime.Now;
        }

        private void ІsValidBDate()
        {
            if (AgeValue() > 135 || BDate.CompareTo(DateTime.Now) > 0)
                throw new InvalidPersonDataException("Illegal Data: write real BDay and your age can't be higher than 135");
        }
        private string WestDataSign()
        {
            int month = BDate.Month;
            int day = BDate.Day;
            switch (month)
            {
                case 12: return (day >= 22) ? "Capricorn" : "Sagittarius";
                case 11: return (day >= 23) ? "Sagittarius" : "Scorpio";
                case 10: return (day >= 23) ? "Scorpio" : "Libra";
                case 9: return (day >= 23) ? "Libra" : "Virgo";
                case 8: return (day >= 23) ? "Virgo" : "Leo";
                case 7: return (day >= 23) ? "Leo" : "Cancer";
                case 6: return (day >= 22) ? "Cancer" : "Gemini";
                case 5: return (day >= 22) ? "Gemini" : "Taurus";
                case 4: return (day >= 21) ? "Taurus" : "Aries";
                case 3: return (day >= 21) ? "Aries" : "Pisces";
                case 2: return (day >= 20) ? "Pisces" : "Aquarius";
                case 1: return (day >= 21) ? "Aquarius" : "Capricorn";
                default: return "";
            }
        }

        private string ChineseDataSign()
        {
            return (ChineseDataEnum)((BDate.Year % 12)) + "";
        }

        private int AgeValue()
        {
            if (BDate.Month.CompareTo(DateTime.Now.Month) < 0)
                return DateTime.Now.Year - BDate.Year;
            else if (BDate.Month.CompareTo(DateTime.Now.Month) == 0 && BDate.Day.CompareTo(DateTime.Now.Day) <= 0)
                return DateTime.Now.Year - BDate.Year;
            return DateTime.Now.Year - BDate.Year - 1;
        }

        #endregion


    }

}
