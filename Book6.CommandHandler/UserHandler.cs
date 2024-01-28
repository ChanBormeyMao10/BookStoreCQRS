using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRG.ES;
using MM.ES;
using Book6.Domain;

namespace Book6.CommandHandler
{
    public class UserHandler : IHandleCommand<NewUser>, IHandleCommand<UpdateUser>, IHandleCommand<DeleteUser>
    {
        private readonly IRepository repository;
        public UserHandler(IRepository repository)
        {
            this.repository = repository;
        }
        public CommandResult Handle(NewUser c)
        {
            var user = new Domain.User(c);
            return new CommandResult(repository.Save(user));
        }
        public CommandResult Handle(UpdateUser c)
        {
            var user = repository.GetById<User>(c.Id);
            user.UpdateUser(c);
            return new CommandResult(repository.Save(user));
        }
        public CommandResult Handle(DeleteUser c)
        {
            var user = repository.GetById<User>(c.Id);
            user.DeleteUser(c);
            return new CommandResult(repository.Save(user));
        }
    }
}
