using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;
using BusinessModel;

namespace TestsSuit
{
    [TestClass]
    public class OrganizationTest
    {
        [TestMethod]
        public void GetOrganization()
        {
            //Prueba el comportamiento cuando existe organización.
            OrganizationBLL organizationBll = new OrganizationBLL();
            ResultBM result = organizationBll.GetOrganization(1);
            Assert.IsNotNull(result.GetValue(), "Debería existir");
            Assert.AreEqual(result.GetValue<OrganizationBM>().name, "Organización 01", "Debería ser Organización 01");
        }

        [TestMethod]
        public void GetInexistentOrganization()
        {
            //Prueba el comportamiento cuando no existe organización.
            OrganizationBLL organizationBll = new OrganizationBLL();
            ResultBM result = organizationBll.GetOrganization(99999);
            Assert.IsNull(result.GetValue(), "No debería existir");
        }
    }
}
