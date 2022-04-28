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
using System.Windows.Controls;
using System.Windows.Input;

namespace Practice1._1.ViewModels
{
    class PersonControlViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Window window = Application.Current.MainWindow;
        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime _bDate = DateTime.MinValue;

        private ObservableCollection<Person> _users;
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _removeCommand;
        private RelayCommand<object> _editCommand;
        private RelayCommand<object> _filterCommand;
        private RelayCommand<object> _allCommand;
        private RelayCommand<object> _sortCommand;

        private PersonService personService = new PersonService();

        private string _number = "GUID №";
        private string _fName = "FName";
        private string _sName = "SName";
        private string _email = "email@gmail.com";
        private string _rules = "";

        private string[] _dateArr = { "2001-02-01T00:00:00",  "1940-01-09T00:00:00", "1980-07-14T00:00:00", "2004-07-16T00:00:00", "1989-11-27T00:00:00",
                "2007-05-13T00:00:00", "1995-02-15T00:00:00", "1986-07-08T00:00:00", "2006-10-08T00:00:00", "1976-07-07T00:00:00", "1987-02-02T00:00:00",
                "2002-01-01T00:00:00", "1990-11-01T00:00:00", "1988-06-04T00:00:00","2001-02-01T00:00:00", "1940-01-09T00:00:00", "1980-07-14T00:00:00", 
                "2004-07-16T00:00:00", "1989-11-27T00:00:00", "2007-05-13T00:00:00", "1995-02-15T00:00:00", "1986-07-08T00:00:00", "2006-10-08T00:00:00", 
                "1976-07-07T00:00:00", "1987-02-02T00:00:00","2002-01-01T00:00:00", "1990-11-01T00:00:00", "1988-06-04T00:00:00" };

        private string[] _nameArr = { "Aaren", "Aazaan", "Gian", "Gianmarco", "Girijan", "Jazz", "Jedd", "Kenzi", "Mehraz", "Meshach",
            "Aaren", "Aazaan", "Gian", "Gianmarco", "Girijan", "Jazz", "Jedd", "Kenzi", "Mehraz", "Meshach" };

        private string[] _snameArr = { "Pele", "Phani", "Youcef", "Yanick", "Waqaas", "Wei", "Vuyolwethu", "Zendel", "Montgomery",
            "Orson", "Pele", "Phani", "Youcef", "Yanick", "Waqaas", "Wei", "Vuyolwethu", "Zendel", "Montgomery", "Orson" };

        private Person _selectedPerson = null;
        #endregion

        #region Constructors
        public PersonControlViewModel()
        {
            TxRules = "Instruction and Rules:\n" +
                "Add: Enter Name SurName and Email before add; \n" +
                "Edit: Click on certain row(Person) in table to Edit;\n" +
                "Remove: Click on certain row(Person) in table to Remove;";
            GenaratePersons(50);
            UpdateTable(3);

        }
        #endregion

        #region ValueDataType
        private enum ChineseDataEnum { Monkey, Rooster, Dog, Pig, Rat, Ox, Tiger, Rabbit, Dragon, Snake, Horse, Goat };
        #endregion


        #region Properties

        private void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGridCell dgr = sender as DataGridCell;
            }
        }

        #region RelayCommands
        public RelayCommand<object> SortCommand
        {
            get
            {
                return _sortCommand ?? (_sortCommand = new RelayCommand<object>(_ => UpdateTable(2)));
            }
        }
        public RelayCommand<object> AllCommand
        {
            get
            {
                return _allCommand ?? (_allCommand = new RelayCommand<object>(_ => UpdateTable(3)));
            }
        }


        public RelayCommand<object> FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand = new RelayCommand<object>(_ => UpdateTable(1), CanExecuteFilter));
            }
        }

        public RelayCommand<object> EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new RelayCommand<object>(_ => AddEdit(true), CanExecuteEdit));
            }
        }

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand<object>(_ => AddEdit(false), CanExecuteAdd));
            }
        }

        public RelayCommand<object> RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new RelayCommand<object>(_ => Remove(), CanExecuteRemove));
            }
        }
        #endregion

        public Person SelectedItem
        {
            get { return _selectedPerson; }
            set
            {
                if (value != null)
                {
                    _selectedPerson = value;
                    TxNumber = _selectedPerson.Guid.ToString();
                    TbFName = _selectedPerson.Name;
                    TbSName = _selectedPerson.Surname;
                    TbEmail = _selectedPerson.Email;
                    BDate = _selectedPerson.BDate;
                }

            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public DateTime BDate
        {
            get
            {
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


        public string TxRules
        {
            get { return _rules; }
            set { _rules = value; OnPropertyChanged(); }
        }

        public string TxNumber
        {
            get { return _number; }
            set { _number = value; OnPropertyChanged(); }
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

        private bool CanExecuteFilter(object obj)
        {
            if (Users.Count > 0)
                return true;
            return false;
        }
        private bool CanExecuteAdd(object obj)
        {

            if (!TbFName.Equals("") && !TbSName.Equals("") && !TbEmail.Equals(""))
                return true;
            return false;
        }

        private bool CanExecuteEdit(object obj)
        {

            if (!TxNumber.Equals("") && !TbFName.Equals("") && !TbSName.Equals("") && !TbEmail.Equals(""))
                return true;
            return false;
        }
        private bool CanExecuteRemove(object obj)
        {

            if (!TxNumber.Equals(""))
                return true;
            return false;
        }

        private async Task GenaratePersons(int numberToGenarate)
        {
            Random rnd = new Random();
            for (int i = 0; i < numberToGenarate; i++)
            {             
                string date = _dateArr[rnd.Next(_dateArr.Length)];
                string name = _nameArr[rnd.Next(_nameArr.Length)];
                string sname = _snameArr[rnd.Next(_snameArr.Length)];


                DateTime bDate = DateTime.Parse(date);
                bool isAdult = false;
                string chineseData = "";
                string westData = "";
                bool isBirthday = isBDay(bDate);
                isAdult = AgeValue(bDate) >= 18;

                await Task.Run(() => westData = WestDataSign(bDate));
                await Task.Run(() => chineseData = ChineseDataSign(bDate));
                DBPerson _dbperson = new DBPerson(name, sname, name + sname + "@gmail.com", bDate, isAdult, chineseData, westData, isBirthday);
                await Task.Run(() => personService.AddAsync(_dbperson));
            }

            UpdateTable(3);
        }

        private async Task Remove()
        {

            window.IsEnabled = false;
            try
            {
                await Task.Run(() => personService.Remove(_number));
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {
                window.IsEnabled = true;
            }
            СlearFields();
            UpdateTable(3);

        }

        private async Task AddEdit(bool isNeedEdit)
        {
            if ((!isNeedEdit && _selectedPerson == null) || (isNeedEdit && _selectedPerson != null))
                try
                {
                    window.IsEnabled = false;
                    ІsValidBDate();
                    bool isAdult = false;
                    string chineseData = "";
                    string westData = "";
                    bool isBirthday = isBDay(BDate);

                    if (isBirthday)
                        MessageBox.Show("Happy B-Day!!!");

                    isAdult = AgeValue(BDate) >= 18;
                    await Task.Run(() => westData = WestDataSign(BDate));
                    await Task.Run(() => chineseData = ChineseDataSign(BDate));

                    if (isNeedEdit)
                    {
                        Person _person = new Person(Guid.Parse(TxNumber), _fName, _sName, _email, BDate, isAdult, chineseData, westData, isBirthday);
                        await Task.Run(() => personService.UpdateAsync(_person));
                        _selectedPerson = null;
                    }
                    else if (!isNeedEdit)
                    {
                        DBPerson _dbperson = new DBPerson(_fName, _sName, _email, BDate, isAdult, chineseData, westData, isBirthday);
                        await Task.Run(() => personService.AddAsync(_dbperson));
                    }

                    UpdateTable(3);
                }
                catch (InvalidPersonDataException ex)
                {
                    MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (InvalidDateException ex)
                {
                    MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                finally
                {
                    window.IsEnabled = true;
                }

            СlearFields();
        }

        private void UpdateTable(int key)
        {
            switch (key)
            {
                case 1:
                    Users = new ObservableCollection<Person>(personService.GetFilterUsers());
                    break;
                case 2:
                    Users = new ObservableCollection<Person>(personService.SortByNamesUsers());
                    break;
                default:
                    Users = new ObservableCollection<Person>(personService.GetAllUsers());
                    break;
            }

        }

        private void СlearFields()
        {
            TxNumber = "";
            TbFName = "";
            TbSName = "";
            TbEmail = "";
            BDate = DateTime.Now;
        }
        private bool isBDay(DateTime bDate)
        {
            if (DateTime.Now.Month == bDate.Month && DateTime.Now.Day == bDate.Day)
            {
                return true;

            }
            return false;
        }

        private void ІsValidBDate()
        {
            if (AgeValue(BDate) > 135 || BDate.CompareTo(DateTime.Now) > 0)
                throw new InvalidPersonDataException("Illegal Data: write real BDay and your age can't be higher than 135");
        }
        private string WestDataSign(DateTime bDate)
        {
            int month = bDate.Month;
            int day = bDate.Day;
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

        private string ChineseDataSign(DateTime bDate)
        {
            return (ChineseDataEnum)((bDate.Year % 12)) + "";
        }

        private int AgeValue(DateTime bDate)
        {
            if (bDate.Month.CompareTo(DateTime.Now.Month) < 0)
                return DateTime.Now.Year - bDate.Year;
            else if (bDate.Month.CompareTo(DateTime.Now.Month) == 0 && bDate.Day.CompareTo(DateTime.Now.Day) <= 0)
                return DateTime.Now.Year - bDate.Year;
            return DateTime.Now.Year - bDate.Year - 1;
        }

        #endregion


    }

}
