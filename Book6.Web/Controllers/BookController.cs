using Book6.ReadModel;
using CRG.ES.CommandProcessor;
using MM.FileStore;
using NLog;
using Raven.Client;
using Raven.Client.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Book6.Web.Controllers
{
    public class BookController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ICommandProcessingUnit cpu;


        private readonly IDocumentSession session;

        public BookController(IDocumentSession session, ICommandProcessingUnit cpu)
        {
            this.session = session;
            this.cpu = cpu;

        }
        public ActionResult Index()
        {
            var books = session.Query<Book>().ToList();
            ViewBag.Users = session.Query<User>().ToList();
            ViewBag.Employees = session.Query<Employee>().ToList();
            return View(books);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NewBook m)
        {
            cpu.Process(new NewBook
            {
                Id = Guid.NewGuid(),
                Title = m.Title,
                IsReserved = false,
                IsInWaitlist = false,
            });
            return RedirectToAction("Index");
        }

        public ActionResult Update(Guid? Id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(UpdateBook m)
        {

            //var book = session.Load<Book>(m.Id);
            cpu.Process(new UpdateBook
            {
                Id = m.Id,
                Title = m.Title,

            });
            return RedirectToAction("Index");
        }

        public ActionResult Delete(DeleteBook m)
        {
            cpu.Process(new DeleteBook
            {
                Id = m.Id,
            });
            return RedirectToAction("Index");
        }

        public ActionResult UpdateReserve(Guid Id)
        {
            cpu.Process(new UpdateBookReservationStatus()
            {
                Id = Id,
            });
            return RedirectToAction("Index", "Reservation");
        }
        public ActionResult UpdateWaitlist(Guid Id)
        {
            cpu.Process(new UpdateBookWaitlistStatus()
            {
                Id = Id,
            });
            return RedirectToAction("Index", "Waitlist");
        }
    }
}