﻿using System;
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

        #region Fileds
        private String _name;
        private String _surname;
        private String _email;
        private DateTime _bDate;
        #endregion


        #region Constructors
        public Person(Guid guid, String name, String surname, String email, DateTime bDate)
        {
            Guid = guid;
            Name = name;
            Surname = surname;
            Email = email;
            BDate = bDate;
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
        public String Name
        {
            get { return _name; }
            private set { _name = value; }
        }
        public String Surname
        {
            get { return _surname; }
            private set { _surname = value; }
        }
        public String Email
        {
            get { return _email; }
            private set { _email = value; }
        }
        public DateTime BDate
        {
            get { return _bDate; }
            private set { _bDate = value; }
        }
        #endregion

    }
}
