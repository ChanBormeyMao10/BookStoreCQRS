using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book6.Common;
using System.Runtime.InteropServices;

namespace Book6.Domain
{
    public class Book : AggregateBase
    {
        public Book()
        {

        }
        public Book(NewBook msg) : this()
        {
            RaiseEvent(msg);
        }
        public void UpdateBook(UpdateBook msg)
        {

            if (msg.Id != Id)
            {
                throw new DomainAggregateException(this, "BookID doesn't exist");
            }

            RaiseEvent(msg);
        }
        public void DeleteBook(DeleteBook msg)
        {
            if (msg.Id != Id)
            {
                throw new DomainAggregateException(this, "BookID doesn't exist");
            }
            RaiseEvent(msg);
        }
        public void UpdateBookReservationStatus(UpdateBookReservationStatus msg)
        {
            if (msg.Id != Id)
            {
                throw new DomainAggregateException(this, "BookID doesn't exist");
            }
            RaiseEvent(msg);
        }
        public void UpdateBookWaitlistStatus(UpdateBookWaitlistStatus msg)
        {
            if (msg.Id != Id)
            {
                throw new DomainAggregateException(this, "BookId doesn't exist");
            }
            RaiseEvent(msg);
        }

        //_________________________________________________________________________________


        private void Apply(NewBook e)
        {
            //var book = new Common.Book
            //{
            //    Id = e.Id,
            //    Title = e.Title,
            //    IsReserved = e.IsReserved,
            //    IsInWaitlist = e.IsInWaitlist,
            //};
            Id = e.Id;

            //_allBooks.Add(book);
        }
        private void Apply(UpdateBook e)
        {
            Id = e.Id;
        }
        private void Apply(DeleteBook e)
        {
            Id = e.Id;
        }
        private void Apply(UpdateBookReservationStatus e)
        {
            Id = e.Id;
        }
        private void Apply(UpdateBookWaitlistStatus e)
        {
            Id = e.Id;
        }


    }
}
