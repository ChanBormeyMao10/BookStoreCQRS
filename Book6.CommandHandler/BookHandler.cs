using CRG.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.ES;
using Book6.Domain;

namespace Book6.CommandHandler
{
    public class BookHandler : IHandleCommand<NewBook>, IHandleCommand<UpdateBook>, IHandleCommand<DeleteBook>, IHandleCommand<UpdateBookReservationStatus>, IHandleCommand<UpdateBookWaitlistStatus>
    {
        private readonly IRepository repository;
        public BookHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public CommandResult Handle(NewBook c)
        {
            var book = new Domain.Book(c);
            return new CommandResult(repository.Save(book));
        }
        public CommandResult Handle(UpdateBook c)
        {
            var book = repository.GetById<Book>(c.Id);
            book.UpdateBook(c);
            return new CommandResult(repository.Save(book));
        }
        public CommandResult Handle(DeleteBook c)
        {
            var book = repository.GetById<Book>(c.Id);
            book.DeleteBook(c);
            return new CommandResult(repository.Save(book));
        }
        public CommandResult Handle(UpdateBookReservationStatus c)
        {
            var book = repository.GetById<Book>(c.Id);
            book.UpdateBookReservationStatus(c);
            return new CommandResult(repository.Save(book));
        }
        public CommandResult Handle(UpdateBookWaitlistStatus c)
        {
            var book = repository.GetById<Book>(c.Id);
            book.UpdateBookWaitlistStatus(c);
            return new CommandResult(repository.Save(book));
        }
    }
}
