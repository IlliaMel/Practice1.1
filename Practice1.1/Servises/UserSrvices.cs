using Practice1._1.Models;
using Practice1._1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice1._1.Services
{
    class UserService
    {
        private static FileRepository Repository = new FileRepository();

        public List<Person> GetAllUsers()
        {
            var res = new List<Person>();
            foreach (var user in Repository.GetAll())
            {
                res.Add(new Person(user.Guid, user.Name, user.Surname, user.Email, user.BDate));
            }
            return res;
        }
    }
}
