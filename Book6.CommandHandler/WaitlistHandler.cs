
using Book6.Domain;
using CRG.ES;
using MM.ES;


namespace Book6.CommandHandler
{
    public class WaitlistHandler : IHandleCommand<NewWaitlist>, IHandleCommand<RemoveWaitlist>
    {
        private readonly IRepository repository;
        public WaitlistHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public CommandResult Handle(NewWaitlist c)
        {
            var w = new Domain.Waitlist(c);
            return new CommandResult(repository.Save(w));
        }
        public CommandResult Handle(RemoveWaitlist c)
        {
            var w = repository.GetById<Waitlist>(c.Id);
            w.RemoveWaitlist(c);
            return new CommandResult(repository.Save(w));
        }
    }
}
