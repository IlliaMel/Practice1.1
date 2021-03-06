using System;
namespace Practice1._1.Models
{
    class Date
    {
        #region Fields
        private DateTime _bDate;
        private DateTime _currentDate;
        #endregion

        #region Constructors
        public Date(DateTime currentDate)
        {
            _currentDate = currentDate;
        }
        #endregion

        #region Properties
        public DateTime BDate
        {
            get
            {
                return _bDate;
            }
            set
            {
                _bDate = value;
            }
        }

        public DateTime СurrentDate
        {
            get
            {
                return _currentDate;
            }
            set
            {
                _currentDate = value;
            }
        }
        #endregion
    }
}
