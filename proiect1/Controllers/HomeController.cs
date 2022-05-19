using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace proiect1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaConsultatii()
        {
            return View();
        }

        public void test(string param)
        {
            string a = param;
            int aaa = 2;
        }

        public ActionResult Login_User(FormCollection form)
        {

            var cnp = form.Get("cnp_input");
            var password = form.Get("password");

            using(var context = new ProiectEHEntities1())
            {
                var citit = context.Pacients.Where(x => x.CNP == cnp && x.Parola == password).FirstOrDefault();
                if (citit != null)
                {
                    //este pacient
                    var a = new Models.CPacient();
                    a.ID = citit.ID;
                    a.CNP = citit.CNP;
                    a.NrBuletin = citit.NrBuletin;
                    a.SerieBuletin = citit.SerieBuletin;
                    a.Mail = citit.Mail;
                    a.Nume = citit.Nume;
                    a.Prenume = citit.Prenume;

                    Session["user"] = a;
                    FormsAuthentication.SetAuthCookie(cnp, false);

                    return RedirectToAction("ListaConsultatii");
                }
                else
                {
                    var citit2 = context.Medics.Where(x => x.CNP == cnp && x.Parola == password).FirstOrDefault();

                    if (citit2 != null)
                    {
                        //medic
                        var a = new Models.CMedic();
                        a.ID = citit2.ID;
                        a.CNP = citit2.CNP;
                        a.Nume = citit2.Nume;
                        a.Prenume = citit2.Prenume;
                        a.Serie_Parafa = citit2.Serie_Parafa;
                        a.Specializare = citit2.Specializare;

                        Session["user"] = a;
                        FormsAuthentication.SetAuthCookie(cnp, false);

                        return RedirectToAction("ListaPacienti", "Medic");

                    }
                }
                return RedirectToAction("Index");

            }



        }

        public ActionResult Register_User(Models.CPacient param)
        {
            var nume = param.Nume;
            var prenume = param.Prenume;
            var cnp = param.CNP;
            var seria = param.SerieBuletin;
            var nr = param.NrBuletin;
            var parola = param.Parola;
            var mail = param.Mail;


            using (var context = new ProiectEHEntities1())
            {
                var citit = context.Pacients.Where(x => x.CNP == cnp).FirstOrDefault();
                if (citit != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var citit2 = context.Medics.Where(x => x.CNP == cnp).FirstOrDefault();
                    if (citit2 != null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //creare
                        Pacient aux = new Pacient();
                        aux.Mail = mail;
                        aux.CNP = cnp;
                        aux.Nume = nume;
                        aux.Prenume = prenume;
                        aux.SerieBuletin = seria;
                        aux.NrBuletin = nr;
                        aux.Parola = parola;

                        context.Pacients.Add(aux);
                        context.SaveChanges();

                    }

                }

            }

            return RedirectToAction("Index");
        }



        public JsonResult LoadData(JqueryDataTableParam param)
        {

            var result = new List<Models.CConsultatie>();

            using (var context = new ProiectEHEntities1())
            {
                foreach (var item in context.Consultaris)
                {
                    int x = ((Models.CMedic)Session["user"]).ID;

                    if(item.ID_Pacient == x)
                    {

                        var aux = new Models.CConsultatie();

                        aux.ID = item.ID;
                        aux.ID_Medic = item.ID_Medic;
                        aux.ID_Pacient = item.ID_Pacient;
                        aux.Data = item.Data;
                        aux.Cost = (float)item.Cost;

                        var nume_med = context.Medics.Where(p => p.ID == aux.ID_Medic).FirstOrDefault();
                        aux.Nume_Medic = nume_med.Nume;
                        aux.Nume_Medic += " ";
                        aux.Nume_Medic += nume_med.Prenume;

                        var nume_pac = context.Pacients.Where(z => z.ID == aux.ID_Pacient).FirstOrDefault();
                        aux.Nume_pacient = nume_pac.Nume;
                        aux.Nume_pacient += " ";
                        aux.Nume_pacient += nume_pac.Prenume;

                        aux.Boala = item.Boala;
                        aux.Cauze = item.Cauze;
                        aux.Simptome = item.Simptome;
                        aux.Analize_recomandate = item.Analize_recomandate;
                        aux.Prescriptie_medicala = item.Prescriptie_medicala;
                    }

                }
            }

            result = result.OrderByDescending(x => x.ID).ToList();
            var sortColumnIndex = Convert.ToInt32(param.iSortCol_0);
            var sortDirection = param.sSortDir_0;

            //if (!string.IsNullOrEmpty(param.sSearch))
            //{
            //    result = result.Where(x => (x.Nume_pacient.ToLower().Contains(param.sSearch.ToLower()))
            //                         ).ToList();
            //}

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

        public ActionResult Delogare()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();

            //// clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            //// clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);



            return RedirectToAction("Index");
        }
    }


}