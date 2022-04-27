using Practice1._1.Models;
using Practice1._1.Repositories;
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
                res.Add(new Person(user.Guid, user.Name, user.Surname, user.Email, user.BDate, user.IsBirthday , user.WestSign , user.ChineseSign , user.IsBirthday));
            }
            return res;
        }


        public bool Remove(string Guid)
        {
            return Repository.Remove(Guid);
        }

        public async Task AddOrUpdateAsync(DBPerson obj)
        {
            await Repository.AddOrUpdateAsync(obj);
        }



    }
}
