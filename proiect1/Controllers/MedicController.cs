using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect1.Controllers
{
    public class MedicController : Controller
    {
        // GET: Medic
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ListaPacienti()
        {
            return View();
        }

        public ActionResult CreareConsultatie()
        {
            return View();
        }

        public ActionResult AdaugaConsultatie(Models.ConsultatieCreare param)
        {
            using (var context = new ProiectEHEntities1())
            {
                //introduc in db un diagnostic

                //introduc o consultatie

            }

            return Json(new { mesaj = "S-a adaugat cu succes!" }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult LoadData(JqueryDataTableParam param)
        {
            var result = new List<Models.CPacient>();

            using (var context = new ProiectEHEntities1())
            {
                foreach (var item in context.Pacients)
                {
                    var aux = new Models.CPacient();

                    aux.ID = item.ID;
                    aux.CNP = item.CNP;
                    aux.Nume = item.Nume;
                    aux.Prenume = item.Prenume;

                    result.Add(aux);
                }
            }

            result = result.OrderByDescending(x => x.ID).ToList();
            var sortColumnIndex = Convert.ToInt32(param.iSortCol_0);
            var sortDirection = param.sSortDir_0;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                result = result.Where(x => (x.CNP.ToLower().Contains(param.sSearch.ToLower()))
                                     || (x.Nume.ToLower().Contains(param.sSearch.ToLower()))
                                     || (x.Prenume.ToLower().Contains(param.sSearch.ToLower()))
                                     ).ToList();
            }

            var displayResult = result.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = result.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);
        }

    }
}