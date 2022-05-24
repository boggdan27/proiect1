using System;
using System.Collections.Generic;
using System.Globalization;
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

        
        public ActionResult DesprePacient()
        {
            return View();
        }

        public ActionResult AdaugaConsultatie(Models.ConsultatieCreare param)
        {

            using (var context = new ProiectEHEntities1())
            {
                var pacient = context.Pacients.Where(x => x.CNP == param.CNP_Pacient).FirstOrDefault();
                if (pacient != null)
                {
                    //am gasit pacientul
                    Consultari cons = new Consultari();
                    cons.ID_Pacient = pacient.ID;
                    cons.Boala = param.Boala;
                    cons.Cauze = param.Cauze;
                    cons.Analize_recomandate = param.Analize_Recomandate;
                    cons.Cost = param.Cost;
                    cons.Data = param.Data;
                    cons.Prescriptie_medicala = param.Prescriptie_Medicala;
                    cons.Simptome = param.Simptome;
                    cons.ID_Medic = ((Models.CMedic)Session["user"]).ID;


                    context.Consultaris.Add(cons);
                    context.SaveChanges();

                    return Json(new { mesaj = "S-a adaugat cu succes!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { mesaj = "Nu s-a gasit pacientul!" }, JsonRequestBehavior.AllowGet);

            }

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


        
        public JsonResult LoadDataPacient(JqueryDataTableParam param)
        {
            var id = param.id_permis;
            var result = new List<Models.CConsultatie>();

            using (var context = new ProiectEHEntities1())
            {
                foreach (var item in context.Consultaris)
                {
                    if (item.ID_Pacient == id)
                    {
                        var aux = new Models.CConsultatie();

                        aux.ID_Pacient = id;
                        aux.ID_Medic = item.ID_Medic;
                        aux.Data = item.Data.ToString("dd'/'MM'/'yyyy");
                        aux.Cost = (float)item.Cost;
                        aux.Boala = item.Boala;
                        aux.Cauze = item.Cauze;
                        aux.Simptome = item.Simptome;
                        aux.Analize_recomandate = item.Analize_recomandate;
                        aux.Prescriptie_medicala = item.Prescriptie_medicala;

                        var pacient = context.Pacients.Where(x => x.ID == item.ID_Pacient).FirstOrDefault();


                        aux.Nume_pacient = pacient.Prenume;
                        aux.Nume_pacient += " ";
                        aux.Nume_pacient += pacient.Nume;

                        result.Add(aux);
                    }

                }
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