using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zegeltjes_Logic;
using Zegeltjes_Models;

namespace Zegeltjes.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void AccountTests()
        {
            AccountLogic accountLogic = new AccountLogic();
            LoginModel loginModel = accountLogic.LogGebruikerIn("robin@live.nl", "Test123");
            Assert.IsNotNull(loginModel.Voornaam);

            loginModel = accountLogic.LogGebruikerIn("robin@live.nl", "Fout wachtwoord");
            Assert.IsNull(loginModel);

            Assert.IsNotNull(accountLogic.RegistreerGebruiker("robin@live.nl", "Test123", "Robin", "Velthuys", "5628KN", "4"));
        }

        [TestMethod]
        public void AanbiedingTests()
        {
            int maxID = 0;
            //Toevoegen
            AanbiedingLogic aanbiedingLogic = new AanbiedingLogic();
            maxID = aanbiedingLogic.TestHelper();
            Assert.AreNotEqual(0, maxID,0,"Kan geen maximumID vinden");//Wil niet doorgaan met testen als dit niet kan
            Assert.IsNull(aanbiedingLogic.VoegAanbiedingToe(2, "TestZegel", 1, "Ruilen"), "Kan geen aanbieding toevoegen");

            //Claimen
            maxID = aanbiedingLogic.TestHelper();
            Assert.AreNotEqual(0, maxID, 0, "Kan geen maximumID vinden");
            Assert.IsFalse(aanbiedingLogic.ClaimAanbieding(maxID, 2), "Aanbieder kan claimen");
            Assert.IsTrue(aanbiedingLogic.ClaimAanbieding(maxID, 4), "Kan geen geldige claim maken");

            //info ophalen
            Aanbieding aanbieding = aanbiedingLogic.HaalAanbiedingOp(maxID);
            Assert.IsNotNull(aanbieding, "Kan aanbieding niet ophalen");

            //Claim toekennen
            Assert.IsTrue(aanbiedingLogic.KenClaimToe(aanbieding.Claims[0].ClaimID), "Kan geen claim toekennen");
            
            //Verwijderen
            Assert.IsFalse(aanbiedingLogic.VerwijderAanbieding(maxID, 4), "Iemand anders kan een aanbieding verwijderen");
            Assert.IsTrue(aanbiedingLogic.VerwijderAanbieding(maxID,2), "Aanbieding kan niet worden verwijderd");
        }

        [TestMethod]
        public void ActieTests()
        {
            ActieLogic actieLogic = new ActieLogic();
            Assert.IsNotNull(actieLogic.HaalHuidigeActiesOp(), "Kan geen acties ophalen");
        }

        [TestMethod]
        public void GeoTests()
        {
            GeoLogic geoLogic = new GeoLogic();
            Assert.IsNotNull(geoLogic.BepaalPlaats("5628KN"), "Kan niet op postcode zoeken");
            Assert.IsNotNull(geoLogic.BepaalPlaats("5628 KN"), "Kan niet op postcode zoeken");
            Assert.IsNotNull(geoLogic.BepaalPlaats("Eindhoven"), "Kan niet op plaats zoeken");
            Assert.IsNull(geoLogic.BepaalPlaats("HALLO"), "krijg een plaats terug die niet bestaat");
        }
    }
}
