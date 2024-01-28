
using Book6.Domain;
using CRG.ES;
using MM.ES;


namespace Book6.CommandHandler
{
    public class EmployeeHandler : IHandleCommand<NewEmployee>, IHandleCommand<UpdateName>, IHandleCommand<UpdateRole>
    {
        private readonly IRepository repository;
        public EmployeeHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public CommandResult Handle(NewEmployee c)
        {
            var employee = new Employee(c);
            return new CommandResult(repository.Save(employee));
        }
        public CommandResult Handle(UpdateName c)
        {
            var employee = repository.GetById<Employee>(c.Id);
            employee.UpdateName(c);
            return new CommandResult(repository.Save(employee));
        }
        public CommandResult Handle(UpdateRole c)
        {
            var employee = repository.GetById<Employee>(c.Id);
            employee.UpdateRole(c);
            return new CommandResult(repository.Save(employee));
        }

    }
}
