using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;

namespace TestsSuit
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void LoginSuccess()
        {
            LoginBLL lbb = new LoginBLL();
            lbb.LogIn("Admin", "Admin");
           // Assert.IsTrue(result.IsValid(), "El usuario debería ser válido");

        }

        [TestMethod]
        public void LoginIncompletePassword()
        {
            LoginBLL lbb = new LoginBLL();
            lbb.LogIn("Admin", "");
            //Assert.IsFalse(result.IsValid(), "Las credeciales deberían ser inválidas");
            //Assert.IsTrue(result.IsCurrentError(ResultDTO.Type.INCOMPLETE_FIELDS));
        }

        [TestMethod]
        public void LoginIncompleteUsername()
        {
            LoginBLL lbb = new LoginBLL();
            lbb.LogIn("", "Admin");
            //Assert.IsFalse(result.IsValid(), "Las credeciales deberían ser inválidas");
            //Assert.IsTrue(result.IsCurrentError(ResultDTO.Type.INCOMPLETE_FIELDS));
        }

        [TestMethod]
        public void LoginWrongPassword()
        {
            LoginBLL lbb = new LoginBLL();
            lbb.LogIn("Admin", "BAD_PASSWORD");
            //Assert.IsFalse(result.IsValid(), "Las credeciales deberían ser inválidas");
            //Assert.IsTrue(result.IsCurrentError(ResultDTO.Type.INVALID_CREDENTIAL));
        }

        [TestMethod]
        public void LoginWrongUsername()
        {
            LoginBLL lbb = new LoginBLL();
            lbb.LogIn("BAD_USERNAME", "Admin");
            //Assert.IsFalse(result.IsValid(), "Las credeciales deberían ser inválidas");
            //Assert.IsTrue(result.IsCurrentError(ResultDTO.Type.INVALID_CREDENTIAL));
        }
    }
}
