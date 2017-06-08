using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;
using BusinessModel;

namespace TestsSuit
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void CreatePerson()
        {
            CountryBLL countryBll = new CountryBLL();
            ResultBM result = countryBll.GetCountry("AR");
            CountryBM countryBm = result.GetValue<CountryBM>();
            AddressBLL addressBll = new AddressBLL();
            AddressBM addressBm = new AddressBM("Calle test", 999, "Departamento", "Barrio", "Esta es una dirección creada mediante test", countryBm);

            PersonBLL personBll = new PersonBLL();
            PersonBM personBm = new PersonBM("Name test", "lastname test", DateTime.Now, "mail", "1553489636", 'M', 29192646, addressBm);
            ResultBM saveResult = personBll.SavePerson(personBm);
            Assert.IsTrue(result.IsValid(), "La persona debería haberse creado");
        }
    }
}
