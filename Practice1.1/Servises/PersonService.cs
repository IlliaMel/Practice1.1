using Practice1._1.Models;
using Practice1._1.Repositories;
using Practice1._1.Tools.MyExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice1._1.Services
{
    class PersonService
    {
        private static FileRepository Repository = new FileRepository();

        public List<Person> GetAllUsers()
        {
            var res = new List<Person>();
            foreach (var user in Repository.GetAll())
            {
                res.Add(new Person(user.Guid, user.Name, user.Surname, user.Email, user.BDate, user.IsAdult , user.WestSign , user.ChineseSign , user.IsBirthday));
            }
            return res;
        }

        public List<Person> GetFilterUsers()
        {
            var res = new List<Person>();
            foreach (var user in Repository.GetAll())
            {
                if (user.IsAdult)
                    res.Add(new Person(user.Guid, user.Name, user.Surname, user.Email, user.BDate, user.IsAdult, user.WestSign, user.ChineseSign, user.IsBirthday));
            }
            return res;
        }


        public void Remove(string Guid)
        {

            if (!Repository.Remove(Guid))
                throw new FileNotFoundException("File Not Found");
                
        }

        public async Task AddAsync(DBPerson obj)
        {
            await Repository.AddAsync(obj);
        }

        public async Task UpdateAsync(Person obj)
        {
            await Repository.UpdateAsync(obj);
        }



    }
}
