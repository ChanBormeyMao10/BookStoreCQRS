using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.Domain
{
    public class User : AggregateBase
    {
        //private List<Book6.Common.User> _allUsers;

        public User()
        {
            //_allUsers = new List<User>();
        }

        public User(NewUser msg) : this()
        {
            if (_allusernames.Contains(msg.Name))
                throw new DomainAggregateException(this, "User Name Existed");
            RaiseEvent(msg);
        }
        public void UpdateUser(UpdateUser msg)
        {

            if (msg.Id != Id)
            {
                throw new DomainAggregateException(this, "User doesn't exist");
            }
            if (_allusernames.Contains(msg.Name))
                throw new DomainAggregateException(this, "User Name Existed");
            RaiseEvent(msg);
        }
        public void DeleteUser(DeleteUser msg)
        {

            if (msg.Id != Id)
            {
                throw new DomainAggregateException(this, "User doesn't exist");
            }
            RaiseEvent(msg);
        }
        //__________________________________________________________________
        public List<string> _allusernames = new List<string>();
        private void Apply(NewUser e)
        {
            Id = e.Id;
            _allusernames.Add(e.Name);
        }
        private void Apply(UpdateUser e)
        {
            Id = e.Id;
            _allusernames.Add(e.Name);
        }
        private void Apply(DeleteUser e)
        {
            Id = e.Id;
        }


    }

}
