using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;
using BusinessModel;

namespace TestsSuit
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateUser()
        {
            string fecha = DateTime.Now.ToString("yyyymmddHHmm");
            UserBLL userBll = new UserBLL();
            UserBM newUser = new UserBM("Usuario " + fecha, true, 1, "GE999", "123");
            ResultBM result =  userBll.CreateUser(newUser);
            Assert.IsTrue(result.IsValid(), "El usuario debería haberse creado");
        }
    }
}
