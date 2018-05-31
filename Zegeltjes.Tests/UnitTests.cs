using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zegeltjes_Logic;
using Zegeltjes_Models;

namespace Zegeltjes.Tests
{
    [TestClass]
    public class UnitTests
    {
        AccountLogic accountLogic = new AccountLogic();
        AanbiedingLogic aanbiedingLogic = new AanbiedingLogic();
        private int MaxID = 0;

        private void Toevoegen()
        {
            aanbiedingLogic.VoegAanbiedingToe(2, "TestZegel", 1, "Ruilen");
        }

        private void Verwijder()
        {
            aanbiedingLogic.VerwijderAanbieding(MaxID, 2);
        }

        [TestMethod]
        public void TestLogin()
        {
            LoginModel loginModel = accountLogic.LogGebruikerIn("robin@live.nl", "Test123");
            Assert.IsNotNull(loginModel.Voornaam);
        }

        [TestMethod]
        public void TestRegister()
        {
            LoginModel loginModel = accountLogic.LogGebruikerIn("robin@live.nl", "Fout wachtwoord");
            Assert.IsNull(loginModel);

            Assert.IsNotNull(accountLogic.RegistreerGebruiker("robin@live.nl", "Test123", "Robin", "Velthuys", "5628KN", "4"));
        }

        [TestMethod]
        public void VoegAanbiedingToe()
        {
            MaxID = aanbiedingLogic.TestHelper();
            Assert.AreNotEqual(0, MaxID, 0, "Kan geen maximumID vinden");//Wil niet doorgaan met testen als dit niet kan
            Assert.IsNull(aanbiedingLogic.VoegAanbiedingToe(2, "TestZegel", 1, "Ruilen"), "Kan geen aanbieding toevoegen");
            Verwijder();
        }

        [TestMethod]
        public void VerwijderAanbieding()
        {
            Toevoegen();
            MaxID = aanbiedingLogic.TestHelper();
            Assert.IsFalse(aanbiedingLogic.VerwijderAanbieding(MaxID, 4), "Iemand anders kan een aanbieding verwijderen");
            Assert.IsTrue(aanbiedingLogic.VerwijderAanbieding(MaxID, 2), "Aanbieding kan niet worden verwijderd");
        }
        
        [TestMethod]
        public void ClaimAanbieding()
        {
            Toevoegen();
            MaxID = aanbiedingLogic.TestHelper();
            Assert.AreNotEqual(0, MaxID, 0, "Kan geen maximumID vinden");
            Assert.IsFalse(aanbiedingLogic.ClaimAanbieding(MaxID, 2), "Aanbieder kan claimen");
            Assert.IsTrue(aanbiedingLogic.ClaimAanbieding(MaxID, 4), "Kan geen geldige claim maken");
            Verwijder();
        }

        [TestMethod]
        public void HaalInfoOp()
        {
            Toevoegen();
            MaxID = aanbiedingLogic.TestHelper();
            Aanbieding aanbieding = aanbiedingLogic.HaalAanbiedingOp(MaxID);
            Assert.IsNotNull(aanbieding, "Kan aanbieding niet ophalen");
            Verwijder();
        }

        [TestMethod]
        public void HaalActiesOp()
        {
            ActieLogic actieLogic = new ActieLogic();
            Assert.IsNotNull(actieLogic.HaalHuidigeActiesOp(), "Kan geen acties ophalen");
        }

        [TestMethod]
        public void GeoPostCode()
        {
            GeoLogic geoLogic = new GeoLogic();
            Assert.IsNotNull(geoLogic.BepaalPlaats("5628KN"), "Kan niet op postcode zoeken");
            Assert.IsNotNull(geoLogic.BepaalPlaats("5628 KN"), "Kan niet op postcode zoeken");
        }

        [TestMethod]
        public void GeoText()
        {
            GeoLogic geoLogic = new GeoLogic();
            Assert.IsNotNull(geoLogic.BepaalPlaats("Eindhoven"), "Kan niet op plaats zoeken");
            Assert.IsNull(geoLogic.BepaalPlaats("HALLO"), "krijg een plaats terug die niet bestaat");
        }
    }
}
