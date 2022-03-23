using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Practice1._1.Models;
using Practice1._1.Tools;
using System.Runtime.CompilerServices;
using System.Windows;


namespace Practice1._1.ViewModels
{
    class PersonControlViewModel : INotifyPropertyChanged
    {
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        private RelayCommand<object> _checkCommand;

        private Person _person = new Person();
        private DateTime _bDate = DateTime.MinValue;
        private bool _isBirthday;
        private bool _isAdult;
        private string _chineseData;
        private string _westData;
        private string _fName = "FName";
        private string _sName = "SName";
        private string _email = "Email";
        private enum ChineseDataEnum { Monkey, Rooster, Dog, Pig, Rat, Ox, Tiger, Rabbit, Dragon, Snake, Horse, Goat };
        #endregion

        #region Properties
        public RelayCommand<object> CheckDataCommand
        {
            get
            {
                return _checkCommand ?? (_checkCommand = new RelayCommand<object>(_ => Action(), CanExecute));
            }
        }
        public DateTime BDate
        {
            get{
                if (_bDate == DateTime.MinValue)
                    return DateTime.Now;
                return _bDate;
            }
            set { _bDate = value; OnChanged(); }
        }
        public bool IsBirthday
        {
            get { return _isBirthday; }
            set { _isBirthday = value; OnChanged(); }
        }

        public bool IsAdult
        {
            get { return _isAdult; }
            set { _isAdult = value; OnChanged(); }
        }
        public string ChineseData
        {
            get { return _chineseData; }
            set { _chineseData = value; OnChanged(); }
        }

        public string WestData
        {
            get { return _westData; }
            set { _westData = value; OnChanged(); }
        }
        public string TxSName
        {
            get { return _sName;}
            set { _sName = value; OnChanged(); }
        }

        public string TxFName
        {
            get { return _fName; }
            set { _fName = value; OnChanged(); }
        }

        public string TxEmail
        {
            get { return _email;}
            set { _email = value; OnChanged(); }
        }

        public string TbSName
        {
            get { return _sName; }
            set { _sName = value; OnChanged(); }
        }


        public string TbFName
        {
            get { return _fName; }
            set { _fName = value; OnChanged(); }
        }

        public string TbEmail
        {
            get { return _email; }
            set { _email = value; OnChanged(); }
        }
        #endregion

        protected virtual void OnChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #region BusinessLogic
        private bool CanExecute(object obj)
        {
            if(!TbFName.Equals("") && !TbSName.Equals("") && !TbEmail.Equals(""))
                return true;
            return false;
        }
        private void Action()
        {
            Task.Factory.StartNew(() => AsyncTask());
        }

        private void AsyncTask()
        {
            _person = new Person(_fName, _sName, _email, BDate);
            if (isValidBDate())
            {           
                if (DateTime.Now.Month == BDate.Month && DateTime.Now.Day == BDate.Day)
                {
                    IsBirthday = true;
                    MessageBox.Show("Happy B-Day!!!");
                }

                TxEmail = _email;
                TxFName = _fName;
                TxSName = _sName;
                IsAdult = ageValue() >= 18;
                WestData = WestDataSign();
                ChineseData = ChineseDataSign();
                return;
            }
            clearFields();
        }

        private void clearFields()
        {
            IsAdult = false;
            IsBirthday = false;
            TxEmail = "";
            TxFName = "";
            TxSName = "";
            TbFName = "";
            TbSName = "";
            TbEmail = "";
            WestData = "";
            ChineseData = "";
            BDate = DateTime.Now;
            MessageBox.Show("Illegal Data: write real BDay and your age can't be higher than 135 ");
        }

        private bool isValidBDate()
        {
            if (ageValue() > 135 || BDate.CompareTo(DateTime.Now) > 0)
                return false;
            return true;
        }
        private string WestDataSign()
        {
            int Month = BDate.Month;
            int Day = BDate.Day;
            switch (Month)
            {
                case 12: return (Day >= 22) ? "Capricorn" : "Sagittarius";
                case 11: return (Day >= 23) ? "Sagittarius" : "Scorpio";
                case 10: return (Day >= 23) ? "Scorpio" : "Libra";
                case 9: return (Day >= 23) ? "Libra" : "Virgo";
                case 8: return (Day >= 23) ? "Virgo" : "Leo";
                case 7: return (Day >= 23) ? "Leo" : "Cancer";
                case 6: return (Day >= 22) ? "Cancer" : "Gemini";
                case 5: return (Day >= 22) ? "Gemini" : "Taurus";
                case 4: return (Day >= 21) ? "Taurus" : "Aries";
                case 3: return (Day >= 21) ? "Aries" : "Pisces";
                case 2: return (Day >= 20) ? "Pisces" : "Aquarius";
                case 1: return (Day >= 21) ? "Aquarius" : "Capricorn";
                default: return "";
            }
        }

        private string ChineseDataSign()
        {
            return (ChineseDataEnum)((BDate.Year % 12)) + "";
        }

        private int ageValue()
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
