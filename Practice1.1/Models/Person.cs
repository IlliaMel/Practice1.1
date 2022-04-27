using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Practice1._1.Tools.MyExceptions;
namespace Practice1._1.Models
{
    class Person
    {


        #region Constructors
        public Person(Guid guid, String name, String surname, String email, DateTime bDate, bool isAdult, string westSign, string chineseSign, bool isBirthday)
        {
            Guid = guid;
            Name = name;
            Surname = surname;
            Email = email;
            BDate = bDate;
            IsAdult = isAdult;
            WestSign = westSign;
            ChineseSign = chineseSign;
            IsBirthday = isBirthday;
        }

        public Person(String name, String surname, String email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }

        public Person(String name, String surname, DateTime bDate)
        {
            Name = name;
            Surname = surname;
            BDate = bDate;
        }

        public Person()
        {
        }
        #endregion

        #region Properties

        public Guid Guid { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public DateTime BDate { get; }
        public bool IsAdult { get; }
        public string WestSign { get; }
        public string ChineseSign { get; }
        public bool IsBirthday { get; }

        #endregion

    }
}
