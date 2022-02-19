using System;
using System.ComponentModel;
using Practice1._1.Models;
using Practice1._1.Tools;
using System.Runtime.CompilerServices;
using System.Windows;


namespace Practice1._1.ViewModels
{
    class DataControlViewModel : INotifyPropertyChanged
    {
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        private RelayCommand<object> _checkCommand;
        private Date _date = new Date(DateTime.Today);
        private string _age;
        private string _chineseData;
        private string _westData;
        private enum ChineseDataEnum { Monkey, Rooster, Dog, Pig, Rat, Ox, Tiger, Rabbit, Dragon, Snake, Horse, Goat };
        #endregion

        #region Properties
        public RelayCommand<object> CheckDataCommand
        {
            get
            {
                return _checkCommand ?? (_checkCommand = new RelayCommand<object>(_ => Check(), CanExecute));
            }
        }
        public DateTime BDate
        {
            get { if (_date.BDate == DateTime.MinValue)
                    return DateTime.Now;
                return _date.BDate; }
            set { _date.BDate = value; OnChanged(); }
        }

        public string Age
        {
            get { return _age; }
            set { _age = value; OnChanged(); }
        }

        public string WestData
        {
            get { return _westData; }
            set { _westData = value; OnChanged(); }
        }

        public string ChineseData
        {
            get { return _chineseData; }
            set { _chineseData = value; OnChanged(); }
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
            return true;
        }
        private void Check()
        {
            if (isValidBDate())
            {
                if(_date.СurrentDate.Month == BDate.Month && _date.СurrentDate.Day == BDate.Day)
                    MessageBox.Show("Happy B-Day!!!");
                Age = ageValue().ToString();
                WestData = WestDataSign();
                ChineseData = ChineseDataSign();
                return;
            }
            clearFields();                   
        }

        private void clearFields()
        {
            Age = "";
            WestData = "";
            ChineseData = "";
            BDate = DateTime.Now;
            MessageBox.Show("Illegal Data: write real BDay and your age can't be higher than 135 ");
        }
    
        private bool isValidBDate()
        {
            if(ageValue() > 135 || BDate.CompareTo(_date.СurrentDate) > 0)
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
            if (BDate.Month.CompareTo(_date.СurrentDate.Month) < 0)
                return _date.СurrentDate.Year - BDate.Year;
            else if (BDate.Month.CompareTo(_date.СurrentDate.Month) == 0 && BDate.Day.CompareTo(_date.СurrentDate.Day) <= 0)
                return _date.СurrentDate.Year - BDate.Year;
           return _date.СurrentDate.Year - BDate.Year - 1;
        }

        #endregion

        

    }
}
