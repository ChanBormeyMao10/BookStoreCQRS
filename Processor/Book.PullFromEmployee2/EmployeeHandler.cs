using Book6.ReadModel;
using CRG.ES.CommandProcessor;
using Employee2;
using MM.ES;
using NLog;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.PullFromEmployee2
{
    public class EmployeeHandler : IHandleEvent<Employee2.NewEmployee>, IHandleEvent<Employee2.UpdateName>, IHandleEvent<Employee2.UpdateRole>
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ICommandProcessingUnit cpu;
        private readonly IDocumentSession session;
        public EmployeeHandler(ICommandProcessingUnit cpu, IDocumentSession session)
        {
            this.cpu = cpu;
            this.session = session;
        }
        public void Handle(Employee2.NewEmployee m)
        {
            cpu.Process(new NewEmployee()
            {
                Id = Guid.NewGuid(),
                Name = m.Name,
                role = m.role,
            });
            logger.Debug(string.Format("New Employee Added Succesfully {0}={1}", m.Id, m.Name));
        }
        public void Handle(Employee2.UpdateName m)
        {
            var e = session.Load<Employee>(m.Id);
            if (e == null) return;
            cpu.Process(new UpdateName()
            {
                Id = m.Id,
                NewName = m.NewName,
            });
            logger.Debug(string.Format("Update Name Succesfully {0}={1}", m.Id, m.NewName));
        }
        public void Handle(Employee2.UpdateRole m)
        {
            var e = session.Load<Employee>(m.Id);
            if (e == null) return;
            cpu.Process(new UpdateRole()
            {
                Id = m.Id,
                NewRole = m.NewRole,
            });
            logger.Debug(string.Format("Update Role Succesfully {0}={1}", m.Id, m.NewRole));
        }

    }
}
