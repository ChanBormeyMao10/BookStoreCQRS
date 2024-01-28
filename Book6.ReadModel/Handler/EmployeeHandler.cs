using Employee2;
using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModel.Handler
{
    public class EmployeeHandler : IHandleEvent<NewEmployee>, IHandleEvent<UpdateName>, IHandleEvent<UpdateRole>
    {
        private readonly IReadModelSession session;
        public EmployeeHandler(IReadModelSession session)
        {
            this.session = session;
        }
        public void Handle(NewEmployee e)
        {
            var emp = new Employee
            {
                Id = e.Id,
                Name = e.Name,
                role = e.role,
            };
            session.Store(emp);
        }
        public void Handle(UpdateRole e)
        {
            var emp = session.Load<Employee>(e.Id);
            emp.role = e.NewRole;
        }
        public void Handle(UpdateName e)
        {
            var emp = session.Load<Employee>(e.Id);
            emp.Name = e.NewName;
        }
    }
}
