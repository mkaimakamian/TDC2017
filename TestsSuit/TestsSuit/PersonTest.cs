﻿using System;
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
            create_person();
        }

        [TestMethod]
        public void GetPerson()
        {
            PersonBM personBm = create_person();
            PersonBLL personBll = new PersonBLL();
            ResultBM personResult = personBll.GetPerson(personBm.id);

            Assert.IsTrue(personResult.IsValid(), "La persona debería haberse recuperado");
            Assert.AreEqual(personResult.GetValue<PersonBM>().id, personBm.id, "Los ids deberían coincidir.");
            Assert.AreEqual(personResult.GetValue<PersonBM>().name, personBm.name, "Los nombres deberían coincidir");
        }


        private PersonBM create_person()
        {
            CountryBLL countryBll = new CountryBLL();
            ResultBM result = countryBll.GetCountry("AR");
            CountryBM countryBm = result.GetValue<CountryBM>();
            AddressBLL addressBll = new AddressBLL();
            AddressBM addressBm = new AddressBM("Calle test", 999, "Departamento", "Barrio", "Esta es una dirección creada mediante test", countryBm);

            PersonBLL personBll = new PersonBLL();
            PersonBM personBm = new PersonBM("Name test", "lastname test", DateTime.Now, "mail", "1553489636", 'M', 29192646, addressBm);
            ResultBM saveResult = personBll.SavePerson(personBm);
            Assert.IsTrue(saveResult.IsValid(), "La persona debería haberse creado");
            return saveResult.GetValue<PersonBM>();
        }

        [TestMethod]
        public void CreatePersonInvalidName()
        {
            PersonBM personBm = new PersonBM("", "lastname test", DateTime.Now, "mail", "1553489636", 'M', 29192646, null);
            ResultBM result = create_invalid_person(personBm);

            Assert.IsTrue(result.IsCurrentError(ResultBM.Type.INCOMPLETE_FIELDS), "No debería haber sido válido.");
            Assert.IsTrue(result.description.Contains("nombre"), "No debería haber sido válido.");
        }

        [TestMethod]
        public void CreatePersonInvalidLastName()
        {
            PersonBM personBm = new PersonBM("name test", "", DateTime.Now, "mail", "1553489636", 'M', 29192646, null);
            ResultBM result = create_invalid_person(personBm);

            Assert.IsTrue(result.IsCurrentError(ResultBM.Type.INCOMPLETE_FIELDS), "No debería haber sido válido.");
            Assert.IsTrue(result.description.Contains("apellido"), "No debería haber sido válido.");
        }


        [TestMethod]
        public void CreatePersonInvalidDNI()
        {
            PersonBM personBm = new PersonBM("name test", "lastname test", DateTime.Now, "mail", "1553489636", 'M', 0, null);
            ResultBM result = create_invalid_person(personBm);

            Assert.IsTrue(result.IsCurrentError(ResultBM.Type.INCOMPLETE_FIELDS), "No debería haber sido válido.");
            Assert.IsTrue(result.description.Contains("dni"), "No debería haber sido válido.");
        }

        private ResultBM create_invalid_person(PersonBM personBm)
        {
            CountryBLL countryBll = new CountryBLL();
            ResultBM result = countryBll.GetCountry("AR");
            CountryBM countryBm = result.GetValue<CountryBM>();
            AddressBLL addressBll = new AddressBLL();
            AddressBM addressBm = new AddressBM("Calle test", 999, "Departamento", "Barrio", "Esta es una dirección creada mediante test", countryBm);

            PersonBLL personBll = new PersonBLL();
            return personBll.SavePerson(personBm);            
        }
    }
}
