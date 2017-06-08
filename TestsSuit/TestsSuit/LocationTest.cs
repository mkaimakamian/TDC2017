using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;
using BusinessModel;

namespace TestsSuit
{
    /// <summary>
    /// Prueba lo referente a los países y direcciones
    /// </summary>
    [TestClass]
    public class LocationTest
    {
        [TestMethod]
        public void GetCountry()
        {
            //Prueba que pueda recuperar el país pasado por parámetro.
            CountryBLL countryBll = new CountryBLL();
            ResultBM result = countryBll.GetCountry("AR");
            CountryBM country = result.GetValue<CountryBM>();
            Assert.IsNotNull(country, "Debería haber recuperado el país argentina");
            Assert.AreEqual(country.iso2, "AR", "Debería existir AR");
            Assert.AreEqual(country.name, "Argentina", "Debería ser Argentina");
        }

        [TestMethod]
        public void GetInexistentCountry()
        {
            //Prueba el comportamiento cuando no existe país.
            CountryBLL countryBll = new CountryBLL();
            ResultBM result = countryBll.GetCountry("XX");
            CountryBM country = result.GetValue<CountryBM>();
            Assert.IsNull(country, "El país con iso2 XX no debería existir");
        }

        [TestMethod]
        public void CreateAddress()
        {
            //Prueba la creación de una dirección
            CountryBLL countryBll = new CountryBLL();
            ResultBM result = countryBll.GetCountry("AR");
            CountryBM countryBm = result.GetValue<CountryBM>();
            AddressBLL addressBll = new AddressBLL();
            AddressBM addressBm = new AddressBM("Calle test", 999, "Departamento", "Barrio", "Esta es una dirección creada mediante test", countryBm);

            ResultBM addressResult = addressBll.SaveAddress(addressBm);
            Assert.IsTrue(addressResult.IsValid(), "Debería haberse guardado");
        }

        [TestMethod]
        public void GetAddress()
        {
            //Prueba la recuperación de una dirección
            CountryBLL countryBll = new CountryBLL();
            ResultBM result = countryBll.GetCountry("AR");
            CountryBM countryBm = result.GetValue<CountryBM>();
            AddressBLL addressBll = new AddressBLL();
            AddressBM addressBm = new AddressBM("Calle test", 999, "Departamento", "Barrio", "Esta es una dirección creada mediante test", countryBm);
            
            ResultBM addressResult = addressBll.SaveAddress(addressBm);
            ResultBM recoveredResult = addressBll.GetAddress(addressResult.GetValue<AddressBM>().id);

            Assert.IsTrue(recoveredResult.IsValid(), "Debería haberse recuperado");
        }

        [TestMethod]
        public void GetInexistentAddress()
        {
            //Prueba la recuperación de una dirección inexistente

            AddressBLL addressBll = new AddressBLL();
            ResultBM recoveredResult = addressBll.GetAddress(999);

            Assert.IsNull(recoveredResult.GetValue(), "No debería existir");
        }

        [TestMethod]
        public void CreateAddressFailsStreet()
        {
            //Prueba la validación de la calle
            CountryBLL countryBll = new CountryBLL();
            ResultBM result = countryBll.GetCountry("AR");
            CountryBM countryBm = result.GetValue<CountryBM>();
            AddressBLL addressBll = new AddressBLL();
            AddressBM addressBm = new AddressBM("", 999, "Departamento", "Barrio", "Esta es una dirección creada mediante test", countryBm);

            ResultBM addressResult = addressBll.SaveAddress(addressBm);
            Assert.IsTrue(addressResult.IsCurrentError(ResultBM.Type.INCOMPLETE_FIELDS), "No debería haber sido válido.");
            Assert.IsTrue(addressResult.description.Contains("dirección"), "No debería haber sido válido.");
        }

        [TestMethod]
        public void CreateAddressFailsNumber()
        {
            //Prueba la validación del número
            CountryBLL countryBll = new CountryBLL();
            ResultBM result = countryBll.GetCountry("AR");
            CountryBM countryBm = result.GetValue<CountryBM>();
            AddressBLL addressBll = new AddressBLL();
            AddressBM addressBm = new AddressBM("Calle test", -1, "Departamento", "Barrio", "Esta es una dirección creada mediante test", countryBm);

            ResultBM addressResult = addressBll.SaveAddress(addressBm);
            Assert.IsTrue(addressResult.IsCurrentError(ResultBM.Type.INCOMPLETE_FIELDS), "No debería haber sido válido.");
            Assert.IsTrue(addressResult.description.Contains("calle"), "No debería haber sido válido.");
        }
       
        [TestMethod]
        public void CreateAddressFailsCountry()
        {
            //Prueba la validación del país
            AddressBLL addressBll = new AddressBLL();
            AddressBM addressBm = new AddressBM("Calle test", 999, "Departamento", "Barrio", "Esta es una dirección creada mediante test", null);

            ResultBM addressResult = addressBll.SaveAddress(addressBm);
            Assert.IsTrue(addressResult.IsCurrentError(ResultBM.Type.INCOMPLETE_FIELDS), "No debería haber sido válido.");
            Assert.IsTrue(addressResult.description.Contains("país"), "No debería haber sido válido.");
        }
    }
}
