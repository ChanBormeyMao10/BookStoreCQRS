
using Book6.ReadModel;
using CRG.ES.CommandProcessor;
using MM.FileStore;
using NLog;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book6.Web.Controllers
{
    public class WaitlistController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ICommandProcessingUnit cpu;


        private readonly IDocumentSession session;

        public WaitlistController(IDocumentSession session, ICommandProcessingUnit cpu)
        {
            this.session = session;
            this.cpu = cpu;
        }
        public ActionResult Index()
        {
            var waitlists = session.Query<Waitlist>().ToList();
            Dictionary<Guid, string> book = new Dictionary<Guid, string>();
            Dictionary<Guid, string> user = new Dictionary<Guid, string>();

            foreach (var w in waitlists)
            {

                book.Add(w.BookId, session.Load<Book>(w.BookId).Title);
                user.Add(w.UserId, session.Load<User>(w.UserId).Name);
            }
            ViewBag.bookWaitlist = book;
            ViewBag.userReservation = user;
            return View(waitlists);
        }



        public ActionResult Create(Guid? BookId, Guid? UserId)
        {
            var book = session.Load<Book>(BookId);
            var user = session.Load<User>(UserId);
            cpu.Process(new NewWaitlist
            {
                Id = Guid.NewGuid(),
                BookId = (Guid)BookId,
                UserId = (Guid)UserId,
            });
            return RedirectToAction("UpdateWaitlist", "Book", new { Id = book.Id });
        }



        public ActionResult Remove(RemoveWaitlist m)
        {
            var reservation = session.Load<ReadModel.Waitlist>(m.Id);
            cpu.Process(new RemoveWaitlist
            {
                Id = m.Id,
            });
            return RedirectToAction("UpdateWaitlist", "Book", new { Id = reservation.BookId });
        }
    }
}