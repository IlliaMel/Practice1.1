using Practice1._1.Tools.MyExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practice1._1.Models
{
    class DBPerson
    {
        public DBPerson(String name, String surname, String email, DateTime bDate, bool isAdult , string westSign , string chineseSign , bool isBirthday)
        {

            if (!Regex.IsMatch(name, "^[A-Z][a-zA-Z]*$"))
                throw new InvalidPersonDataException("Invalid Name");
            if (!Regex.IsMatch(surname, "^[A-Z][a-zA-Z]*$"))
                throw new InvalidPersonDataException("Invalid Surname");
            if (!Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                throw new InvalidPersonDataException("Invalid Email");

            Guid = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            BDate = bDate;
            IsAdult = isAdult;
            WestSign = westSign;
            ChineseSign = chineseSign;
            IsBirthday = isBirthday;
        }

        public Guid Guid { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public DateTime BDate { get; }
        public bool IsAdult { get; }
        public string WestSign { get; }
        public string ChineseSign { get; }
        public bool IsBirthday { get; }


    }
}
