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
                
        [TestMethod]
        public void CreateDonor()
        {
            //Crea un donador
            create_donor();
        }

        [TestMethod]
        public void GetDonor()
        {
            //Crea un donador
            DonorBM donorBm = create_donor();
            DonorBLL donorBll = new DonorBLL();
            ResultBM donorResult = donorBll.GetDonor(donorBm.donorId);
            Assert.IsTrue(donorResult.IsValid(), "El donador debería existir.");

        }

        [TestMethod]
        public void CreateVolunteer()
        {
            //Crea un donador
            PersonBM personBm = create_person();

            BranchBLL branchBll = new BranchBLL();
            ResultBM brancResult = branchBll.GetBranch(1);
            Assert.IsTrue(brancResult.IsValid(), "El donador debería existir.");

            VolunteerBLL volunteerBll = new VolunteerBLL();
            VolunteerBM volunteerBm = new VolunteerBM(personBm, brancResult.GetValue<BranchBM>());
            ResultBM volunterResult = volunteerBll.SaveVolunteer(volunteerBm);
            
            Assert.IsTrue(volunterResult.IsValid(), "El donador debería existir.");
            Assert.IsNotNull(volunterResult.GetValue(), "Debería existir el voluntario.");
            Assert.IsTrue(volunterResult.GetValue<VolunteerBM>().id > 0, "Debería existir el voluntario.");
        }

        [TestMethod]
        public void CreateVolunteerWithUser()
        {
            //Crea un donador
            PersonBM personBm = create_person();

            BranchBLL branchBll = new BranchBLL();
            ResultBM brancResult = branchBll.GetBranch(1);
            Assert.IsTrue(brancResult.IsValid(), "El donador debería existir.");

            UserBLL userBll = new UserBLL();
            ResultBM userResult = userBll.GetUser("Admin", "Admin");

            VolunteerBLL volunteerBll = new VolunteerBLL();
            VolunteerBM volunteerBm = new VolunteerBM(personBm, brancResult.GetValue<BranchBM>(), userResult.GetValue<UserBM>());
            ResultBM volunterResult = volunteerBll.SaveVolunteer(volunteerBm);

            Assert.IsTrue(volunterResult.IsValid(), "El donador debería existir.");
            Assert.IsNotNull(volunterResult.GetValue(), "Debería existir el voluntario.");
            Assert.IsTrue(volunterResult.GetValue<VolunteerBM>().id > 0, "Debería existir el voluntario.");
            Assert.IsNotNull(volunterResult.GetValue<VolunteerBM>().user, "Debería existir el usuario.");
            Assert.IsTrue(volunterResult.GetValue<VolunteerBM>().user.Id > 0, "Debería existir el usuario.");
        }

        [TestMethod]
        public void CreateVolunteerFailsBranch()
        {
            //Crea un donador
            PersonBM personBm = create_person();

            VolunteerBLL volunteerBll = new VolunteerBLL();
            VolunteerBM volunteerBm = new VolunteerBM(personBm, null);
            ResultBM volunterResult = volunteerBll.SaveVolunteer(volunteerBm);

            Assert.IsFalse(volunterResult.IsValid(), "El voluntario debería existir.");
            Assert.IsTrue(volunterResult.IsCurrentError(ResultBM.Type.INCOMPLETE_FIELDS), "No debería haber sido válido.");
            Assert.IsNull(volunterResult.GetValue(), "No debería existir el voluntario.");
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

        private DonorBM create_donor()
        {
            //Crea un donador
            OrganizationBLL organizationBll = new OrganizationBLL();
            ResultBM result = organizationBll.GetOrganization(1);
            PersonBM personBm = create_person();
            DonorBM donorBm = new DonorBM(true, personBm, result.GetValue<OrganizationBM>());
            DonorBLL donorBll = new DonorBLL();
            ResultBM saveResult = donorBll.SaveDonor(donorBm);

            Assert.IsTrue(saveResult.IsValid(), "El donador debería haberse creado.");
            return saveResult.GetValue<DonorBM>();

        }
    }
}
