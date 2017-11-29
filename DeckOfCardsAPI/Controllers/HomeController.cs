using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace DeckOfCardsAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DrawFromDeck()
        {

            HttpWebRequest WR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");

            WR.UserAgent = ".NET Framework Test Client";

            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();

            StreamReader Reader = new StreamReader(Response.GetResponseStream());

            string DeckData = Reader.ReadToEnd();

            JObject JsonData = JObject.Parse(DeckData);

            ViewBag.DeckId = JsonData["deck_id"];

            HttpWebRequest NR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/" + JsonData["deck_id"] + "/draw/?count=5");

            NR.UserAgent = ".NET Framework Test Client";
            HttpWebResponse NewResponse = (HttpWebResponse)NR.GetResponse();
            StreamReader NewReader = new StreamReader(NewResponse.GetResponseStream());

            string DrawData = NewReader.ReadToEnd();

            JObject NewJsonData = JObject.Parse(DrawData);

            ViewBag.Draw = NewJsonData["cards"];
            
            return View();
        }
    }
}