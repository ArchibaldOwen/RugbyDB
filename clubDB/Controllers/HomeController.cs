using clubDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace clubDB.Controllers
{
    public class HomeController : Controller
    {
        private rugbyEntities db = new rugbyEntities();

        public ActionResult Index(string SearchString, string clubCountry)
        {
            List<string> countryList = new List<string>();
            var countryQuery = from c in db.clubs
                               orderby c.Country
                               select c.Country;

            countryList.AddRange(countryQuery.Distinct());

            ViewBag.clubCountry = new SelectList(countryList);

            var clubs = from m in db.clubs
                        select m;

            if (!String.IsNullOrEmpty(clubCountry))
            {
                clubs = clubs.Where(x => x.Country == (clubCountry));
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                clubs = clubs.Where(x => x.Name.Contains(SearchString));
            }

            return View(clubs);
        }

        public ActionResult _TableRow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            club club = db.clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return PartialView(club);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Established,Country,CurrentCoach,Cheer,Video,Logo,Trophies,ClubHomepage,Stadium,Capacity,NotablePlayers,Rivalries")]club club)

        {

            if (club.Logo == null)
            {
                club.Logo = "https://www.shareicon.net/data/2015/10/20/130824_exit_256x256.png";
            }

            if (club.Cheer == null)
            {
                club.Cheer = 0;
            }

            if (ModelState.IsValid)
            {
                db.clubs.Add(club);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(club);
        }

        
        public ActionResult Cheer(int id)
        {
            club clubCheer = db.clubs.Find(id);
            int? currentCheers = clubCheer.Cheer;
            clubCheer.Cheer = currentCheers + 1;
            if (ModelState.IsValid)
            {
                db.Entry(clubCheer).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            return PartialView("_TableRow", clubCheer);
        }

        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            club club = db.clubs.Find(id);


            if (club == null)
            {
                return HttpNotFound();
            }

            return View(club);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Established,Country,CurrentCoach,Video,Cheer,Logo,Trophies,ClubHomepage,Stadium,Capacity,NotablePlayers,Rivalries")]club club)
        {
            if (club.Logo == null)
            {
                club.Logo = "https://www.shareicon.net/data/2015/10/20/130824_exit_256x256.png";
            }

            if (ModelState.IsValid)
            {
                db.Entry(club).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(club);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            club club = db.clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
          

            return View(club);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            club club = db.clubs.Find(id);

            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            club club = db.clubs.Find(id);
            db.clubs.Remove(club);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}