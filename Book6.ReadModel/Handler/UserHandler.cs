using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModel.Handler
{
    public class UserHandler : IHandleEvent<NewUser>, IHandleEvent<UpdateUser>, IHandleEvent<DeleteUser>
    {
        private readonly IReadModelSession session;
        public UserHandler(IReadModelSession session)
        {
            this.session = session;
        }
        public void Handle(NewUser e)
        {
            var user = new User()
            {
                Id = e.Id,
                Name = e.Name,
            };
            session.Store(user);
        }
        public void Handle(UpdateUser e)
        {
            var user = session.Load<User>(e.Id);
            user.Name = e.Name;
        }
        public void Handle(DeleteUser e)
        {
            var user = session.Load<User>(e.Id);
            session.Delete(user);
        }

    }
}
