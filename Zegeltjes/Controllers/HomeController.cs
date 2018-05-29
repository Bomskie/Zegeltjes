using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace Zegeltjes.Controllers
{
    public class HomeController : Controller
    {
        const string SessionName = "_Naam";
        const string SessionId = "_ID";

        public IActionResult Index()
        {
            //DIT VERWIJDEREN NA DEV
           // HttpContext.Session.SetString(SessionName, $"Robin Velthuys");
           // HttpContext.Session.SetInt32(SessionId, 2);
            //EINDE VERWIJDEREN

            return View();
        }

        [HttpGet]
        public IActionResult Zegels(string plaats)
        {
            if (plaats != null)
            {
                Zegeltjes.Models.Aanbiedingen.ZegelsViewModel zegelsViewModel = new Models.Aanbiedingen.ZegelsViewModel();
                Zegeltjes_Logic.ActieLogic actieLogic = new Zegeltjes_Logic.ActieLogic();
                zegelsViewModel.Acties = actieLogic.HaalHuidigeActiesOp();

                Zegeltjes_Logic.AanbiedingLogic aanbiedingLogic = new Zegeltjes_Logic.AanbiedingLogic();
                zegelsViewModel.Aanbiedingen = aanbiedingLogic.HaalAanbiedingenOp(plaats);

                if (zegelsViewModel.Aanbiedingen == null)
                {
                    zegelsViewModel.Aanbiedingen = new System.Collections.Generic.List<Zegeltjes_Models.Aanbieding>();
                }
                if (zegelsViewModel.Acties == null)
                {
                    return RedirectToAction("Index");
                }
                return View(zegelsViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        #region Login
        public IActionResult Login()
        {
            string id = HttpContext.Session.GetInt32("_ID").ToString();
            if (id != "")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Login(Models.Account.LoginModel loginModel)
        {
            Zegeltjes_Logic.AccountLogic accountLogic = new Zegeltjes_Logic.AccountLogic();
            Zegeltjes_Models.LoginModel result = accountLogic.LogGebruikerIn(loginModel.Mail, loginModel.Wachtwoord);
            if (result == null)
            {
                ModelState.AddModelError("1","Ongeldige gebruikersnaam en of wachtwoordCombinatie");
                return View(loginModel);
            }
            else
            {

                HttpContext.Session.SetString(SessionName, $"{result.Voornaam} {result.Achternaam}");
                HttpContext.Session.SetInt32(SessionId, Convert.ToInt32(result.GebruikerID));

                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Models.Account.RegisterModel registerModel)
        {
            Zegeltjes_Logic.AccountLogic accountLogic = new Zegeltjes_Logic.AccountLogic();
            string response = accountLogic.RegistreerGebruiker(registerModel.Mail, registerModel.Wachtwoord, registerModel.Voornaam, registerModel.Achternaam, registerModel.Postcode, registerModel.Postcode);
            if (response == null)
            {
                return RedirectToAction("Inloggen");
            }
            else
            {
                ModelState.AddModelError("1", response);
            }
            return View(registerModel);
        }
        #endregion

        #region Aanbiedingen

        public IActionResult Aanbieding()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Aanbieding(string id)
        {
            try
            {
                int aanbiedingID = Convert.ToInt32(id);

                Models.Aanbiedingen.AanbiedingViewModel aanbiedingModel = new Models.Aanbiedingen.AanbiedingViewModel();
                Zegeltjes_Logic.AanbiedingLogic aanbiedingHelper = new Zegeltjes_Logic.AanbiedingLogic();
                Zegeltjes_Models.Aanbieding res = aanbiedingHelper.HaalAanbiedingOp(aanbiedingID);
                //aanbiedingModel.Claims = aanbiedingHelper.HaalClaimsOp(aanbiedingID);
                if (res != null)
                {
                    aanbiedingModel.Aanbieding = res;
                    if (res.Claims == null)
                    {
                        res.Claims = new System.Collections.Generic.List<Zegeltjes_Models.Claim>();
                    }

                    return View(aanbiedingModel);
                }
                else
                {
                    return RedirectToAction("Index");
                }                
            }
            catch (Exception)
            {
               return RedirectToAction("Index");
            }
        }


        public IActionResult MijnAanbiedingen()
        {
            if (HttpContext.Session.GetString(SessionName) != null)
            {
                Zegeltjes_Logic.AanbiedingLogic aanbiedingHelper = new Zegeltjes_Logic.AanbiedingLogic();
                Zegeltjes.Models.Aanbiedingen.MijnAanbiedingModel mijnAanbiedingModel = new Models.Aanbiedingen.MijnAanbiedingModel();
                // mijnAanbiedingModel.dtMijnAanbiedingen = aanbiedingHelper.HaalMijnAanbiedingenOp(Convert.ToInt32(HttpContext.Session.GetInt32(SessionId)));
                mijnAanbiedingModel.AlleAanbiedingen = aanbiedingHelper.HaalMijnAanbiedingenOp(Convert.ToInt32(HttpContext.Session.GetInt32(SessionId)));
                return View(mijnAanbiedingModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult VoegAanbiedingToe()
        {
            if (HttpContext.Session.GetString(SessionName) != null)
            {
                Zegeltjes_Logic.ActieLogic actieHelper = new Zegeltjes_Logic.ActieLogic();
                Models.Aanbiedingen.VoegAanbiedingToeModel VoegAanbiedingToeModel = new Models.Aanbiedingen.VoegAanbiedingToeModel();
                // VoegAanbiedingToeModel.AlleActies = actieHelper.HaalHuidigeActiesOp();
                VoegAanbiedingToeModel.Acties = actieHelper.HaalHuidigeActiesOp();

                return View(VoegAanbiedingToeModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult VoegAanbiedingToe(Models.Aanbiedingen.VoegAanbiedingToeModel VoegAanbiedingToeModel)
        {
            if (HttpContext.Session.GetString(SessionName) != null)
            {
                Zegeltjes_Logic.AanbiedingLogic aanbiedingHelper = new Zegeltjes_Logic.AanbiedingLogic();
                string result = aanbiedingHelper.VoegAanbiedingToe(Convert.ToInt32(HttpContext.Session.GetInt32(SessionId)), VoegAanbiedingToeModel.omschrijving, Convert.ToInt32(VoegAanbiedingToeModel.actie), VoegAanbiedingToeModel.type);

                if (result == null)
                {
                    return RedirectToAction("MijnAanbiedingen");
                }
                else
                {
                    Zegeltjes_Logic.ActieLogic actieHelper = new Zegeltjes_Logic.ActieLogic();
                    VoegAanbiedingToeModel.Acties = actieHelper.HaalHuidigeActiesOp();
                    return View(VoegAanbiedingToeModel);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Commands zonder view
        [HttpGet]
        public IActionResult VerwijderAanbieding(string id)
        {
            string sessionId = HttpContext.Session.GetInt32("_ID").ToString();
            if (sessionId != "")
            {
                Zegeltjes_Logic.AanbiedingLogic aanbiedingHelper = new Zegeltjes_Logic.AanbiedingLogic();
                if (aanbiedingHelper.VerwijderAanbieding(Convert.ToInt32(id), Convert.ToInt32(HttpContext.Session.GetInt32(SessionId))))
                {
                    return RedirectToAction("MijnAanbiedingen");
                }
                else
                {
                    return RedirectToAction("Aanbieding/" + id);
                }
            }
            else
            {
                return RedirectToAction("Aanbieding/" + id);
            }            
        }

        public IActionResult Claim(string id)
        {
            string sessionId = HttpContext.Session.GetInt32("_ID").ToString();
            if (sessionId != "")
            {
                Zegeltjes_Logic.AanbiedingLogic aanbiedingHelper = new Zegeltjes_Logic.AanbiedingLogic();
                if (aanbiedingHelper.ClaimAanbieding(Convert.ToInt32(id), Convert.ToInt32(HttpContext.Session.GetInt32(SessionId))))
                {
                    return RedirectToAction(id, "Aanbieding");
                }
                else
                {
                    return RedirectToAction(id, "Aanbieding");
                }
            }
            else
            {
                return RedirectToAction(id, "Aanbieding");
            }
        }

        [HttpGet]
        public IActionResult BepaalPlaats(Models.Aanbiedingen.IndexModel indexModel)
        {
            Zegeltjes_Logic.GeoLogic geoLogic = new Zegeltjes_Logic.GeoLogic();
            string plaats = geoLogic.BepaalPlaats(indexModel.Plaats);

            if (plaats != null)
            {
                return RedirectToAction(plaats, "Zegels");
            }
            else
            {
                ModelState.AddModelError("1", "Niks gevonden");
                return View("Index");
            }            
        }

        [HttpGet]
        public IActionResult ClaimToekennen(int id)
        {
            string sessionId = HttpContext.Session.GetInt32("_ID").ToString();
            if (sessionId != "")
            {
                Zegeltjes_Logic.AanbiedingLogic aanbiedingLogic = new Zegeltjes_Logic.AanbiedingLogic();
                aanbiedingLogic.KenClaimToe(id);
            }
            return RedirectToAction("MijnAanbiedingen");
        }
        #endregion
    }
}