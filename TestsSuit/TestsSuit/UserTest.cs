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
            this.create_user();
            
        }

        [TestMethod]
        public void EditUser()
        {
            UserBM user = this.create_user();
            UserBLL userBll = new UserBLL();
            string fecha = DateTime.Now.ToString("yyyymmddHHmm");
            string nombre = "XXX" + fecha;
            user.Name = nombre;
            userBll.UpdateUser(user);
            //Assert.IsTrue(result.IsValid(), "El usuario debería haberse actualizado");
            user = userBll.GetUser(nombre, "123").GetValue<UserBM>();
            Assert.AreEqual(user.Name, nombre);

        }

        private UserBM create_user()
        {
            string fecha = DateTime.Now.ToString("yyyymmddHHmm");
            UserBLL userBll = new UserBLL();
            UserBM newUser = new UserBM("Usuario " + fecha, true, 1, "GE999", "123");
            ResultBM result = userBll.CreateUser(newUser);
            Assert.IsTrue(result.IsValid(), "El usuario debería haberse creado");
            return result.GetValue<UserBM>();
        }
    }
}
