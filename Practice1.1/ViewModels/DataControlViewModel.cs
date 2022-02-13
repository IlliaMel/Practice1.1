using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Practice1._1.Models;
using Practice1._1.Tools;
using System.Runtime.CompilerServices;



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
        #endregion

        #region Properties
        public DateTime BDate
        {
            get{ return _date.BDate; }
            set{ _date.BDate = value; }
        }

        public string Age
        {
            get { return _age;}
            set{ _age = value; OnChanged();}
        }

        public string WestData
        {
            get {return _westData; }
            set { _westData = value; OnChanged();}
        }

        public string ChineseData
        {
            get {return _chineseData; }
            set { _chineseData = value; OnChanged();}
        }

        public RelayCommand<object> CheckDataCommand
        {
            get
            {
                return _checkCommand ?? (_checkCommand = new RelayCommand<object>(_ => Check(), CanExecute));
            }
        }

        private bool CanExecute(object obj)
        {
            return true;
        }

        private void Check()
        {
            Age = "ok";
            WestData = "ok";
            ChineseData = "ok";
            Console.WriteLine("ok");
        }
        #endregion

        protected virtual void OnChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
