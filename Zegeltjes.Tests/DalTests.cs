using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Zegeltjes_DAL;

namespace Zegeltjes.Tests
{
    [TestClass]
    public class DalTests
    {
        [TestMethod]
        public void TestLogin()
        {
            LoginCommand loginCommand = new LoginCommand("robin@live.nl", "Test123");
            Zegeltjes_Models.LoginModel uid = loginCommand.Execute();
            Assert.IsNotNull(uid.GebruikerID);
        }
        /*
        [TestMethod]
        public void TestRegister()
        {
            RegisterCommand registerCommand = new RegisterCommand("robin@live.nl", "Test123", "Robin", "Velthuys", "5628KN", "4", "Schoklandstraat");
            registerCommand.Execute();
        }

        [TestMethod]
        public void TestSelecteerActie()
        {
            SelecteerAlleGeldigeActieCommand selecteerAlleGeldigeActie = new SelecteerAlleGeldigeActieCommand();
            selecteerAlleGeldigeActie.Execute();
        }
        */
    }
}
