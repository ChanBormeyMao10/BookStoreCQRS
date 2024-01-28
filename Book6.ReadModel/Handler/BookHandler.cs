using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModel.Handler
{
    public class BookHandler : IHandleEvent<NewBook>, IHandleEvent<UpdateBook>, IHandleEvent<DeleteBook>, IHandleEvent<UpdateBookReservationStatus>, IHandleEvent<UpdateBookWaitlistStatus>
    {
        private readonly IReadModelSession session;

        public BookHandler(IReadModelSession session)
        {
            this.session = session;
        }
        public void Handle(NewBook e)
        {
            var book = new Book
            {
                Id = e.Id,
                Title = e.Title,
                author = e.author,
                IsInWaitlist = e.IsInWaitlist,
                IsReserved = e.IsReserved,
            };
            session.Store(book);
        }
        public void Handle(UpdateBook e)
        {
            var book = session.Load<Book>(e.Id);
            book.Title = e.Title;
        }
        public void Handle(DeleteBook e)
        {
            var book = session.Load<Book>(e.Id);
            session.Delete(book);
        }
        public void Handle(UpdateBookReservationStatus e)
        {
            var book = session.Load<Book>(e.Id);
            book.IsReserved = !book.IsReserved;
        }
        public void Handle(UpdateBookWaitlistStatus e)
        {
            var book = session.Load<Book>(e.Id);
            book.IsInWaitlist = !book.IsInWaitlist;
        }
    }
}
