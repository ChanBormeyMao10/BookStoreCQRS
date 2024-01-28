using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book6.ReadModel;
using CRG.ES.CommandProcessor;
using MM.FileStore;
using MM.ES;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using NLog;


namespace Book6.Web.Controllers
{
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ICommandProcessingUnit cpu;


        private readonly IDocumentSession session;

        public HomeController(IDocumentSession session, ICommandProcessingUnit cpu)
        {
            this.session = session;
            this.cpu = cpu;

        }
        public ActionResult Index()
        {
            var books = session.Query<User>().ToList();
            return View(books);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(NewBook m)
        {

            //   m.Id = Guid.NewGuid();
            //    m.IsInWaitlist = false;
            //    m.IsReserved = false;
            cpu.Process(new NewBook
            {
                Id = Guid.NewGuid(),
                Title = m.Title,
                IsReserved = false,
                IsInWaitlist = false,
            });

            return RedirectToAction("Index");
        }
    }
}
