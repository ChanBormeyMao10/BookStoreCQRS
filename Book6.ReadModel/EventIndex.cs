using System;
using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Indexes;


namespace Book6.ReadModel.RavenConfig
{
    public class BookEvent_SetupIndex : AbstractIndexCreationTask<Book6.ReadModel.Book, BookEvent_SetupIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public Guid Id;
            public string Title;
            public bool IsReserved;
            public bool IsInWaitlist;
        }

        public BookEvent_SetupIndex()
        {
            Map = books => from book in books
                           select new IndexEntry()
                           {
                               Id = book.Id,
                               Title = book.Title,
                               IsReserved = book.IsReserved,
                               IsInWaitlist = book.IsInWaitlist
                           };
            Index(x => x.Id, FieldIndexing.Analyzed);
        }
    }
    public class UserEvent_SetupIndex : AbstractIndexCreationTask<Book6.ReadModel.User, UserEvent_SetupIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public Guid UserId;
        }

        public UserEvent_SetupIndex()
        {
            Map = users => from user in users
                           select new IndexEntry()
                           {
                               UserId = user.Id
                           };
        }
    }
    public class ReservationEvent_SetupIndex : AbstractIndexCreationTask<Book6.ReadModel.Reservation, ReservationEvent_SetupIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public Guid ReservationId;
        }


        public ReservationEvent_SetupIndex()
        {
            Map = reservations => from reservation in reservations
                                  select new IndexEntry()
                                  {
                                      ReservationId = reservation.Id
                                  };
        }
    }
    public class WaitListEvent_SetupIndex : AbstractIndexCreationTask<Book6.ReadModel.Waitlist, WaitListEvent_SetupIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public Guid WaitListId;
        }


        public WaitListEvent_SetupIndex()
        {
            Map = waitlists => from waitlist in waitlists
                               select new IndexEntry()
                               {
                                   WaitListId = waitlist.Id
                               };
        }
    }
}

