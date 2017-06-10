using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;
using BusinessModel;

namespace TestsSuit
{
    [TestClass]
    public class DonationTest
    {
        [TestMethod]
        public void CreateDonation()
        {
            //Crea una donación
            DonorBM donor = create_donor();
            DonationStatusBM statusBm = get_status(1);
            DonationBLL donationBll = new DonationBLL();
            DonationBM donationBm = new DonationBM(21, donor.donorId, statusBm, "Esta es una donación creada por un test.");

            ResultBM donationResult = donationBll.SaveDonation(donationBm);
            Assert.IsTrue(donationResult.IsValid(), "La donación debería ser válida.");
            Assert.IsNotNull(donationResult.GetValue(), "Deería haber devuelto una donación.");
            Assert.IsTrue(donationResult.GetValue<DonationBM>().id > 0, "El id debería ser mayor a cero.");
            Assert.AreEqual(donationResult.GetValue<DonationBM>().comment, "Esta es una donación creada por un test.", "Debería poseer comentario");
            
        }

        [TestMethod]
        public void CreateDonationNullComment()
        {
            //Crea una donación
            DonorBM donor = create_donor();
            DonationStatusBM statusBm = get_status(1);
            DonationBLL donationBll = new DonationBLL();
            DonationBM donationBm = new DonationBM(21, donor.donorId, statusBm);

            ResultBM donationResult = donationBll.SaveDonation(donationBm);
            Assert.IsTrue(donationResult.IsValid(), "La donación debería ser válida.");
            Assert.IsNotNull(donationResult.GetValue(), "Deería haber devuelto algo.");
            Assert.IsNull(donationResult.GetValue<DonationBM>().comment, "No debería poseer comentario");
        }

        [TestMethod]
        public void CreateDonationFailsDonor()
        {
            //Prueba la validación cuando no se provee donador válido           
            DonationStatusBM statusBm = get_status(1);
            DonationBLL donationBll = new DonationBLL();
            DonationBM donationBm = new DonationBM(21, 0, statusBm);

            ResultBM donationResult = donationBll.SaveDonation(donationBm);
            Assert.IsFalse(donationResult.IsValid(), "La donación no debería ser válida.");
            Assert.IsNotNull(donationResult.IsCurrentError(ResultBM.Type.INCOMPLETE_FIELDS), "El error debería ser campo incompleto.");
            Assert.AreEqual(donationResult.description, "Debe asignarse donador.", "El error debería coincidir.");
        }

        [TestMethod]
        public void CreateDonationFailsItems()
        {
            //Prueba la validación cuando la cantidad de items no es la menos 1
            DonorBM donor = create_donor();
            DonationStatusBM statusBm = get_status(1);
            DonationBLL donationBll = new DonationBLL();
            DonationBM donationBm = new DonationBM(0, donor.donorId, statusBm);

            ResultBM donationResult = donationBll.SaveDonation(donationBm);
            Assert.IsFalse(donationResult.IsValid(), "La donación no debería ser válida.");
            Assert.IsNotNull(donationResult.IsCurrentError(ResultBM.Type.INCOMPLETE_FIELDS), "El error debería ser campo incompleto.");
            Assert.AreEqual(donationResult.description, "La cantidad de bultos debe ser de al menos una unidad.", "El error debería coincidir.");
        }

        [TestMethod]
        public void CreateDonationFailsStatus()
        {
            //Prueba la validación cuando el estado no es provisto
            DonorBM donor = create_donor();
            DonationBLL donationBll = new DonationBLL();
            DonationBM donationBm = new DonationBM(2, donor.donorId, null);

            ResultBM donationResult = donationBll.SaveDonation(donationBm);
            Assert.IsFalse(donationResult.IsValid(), "La donación no debería ser válida.");
            Assert.IsNotNull(donationResult.IsCurrentError(ResultBM.Type.INCOMPLETE_FIELDS), "El error debería ser campo incompleto.");
            Assert.AreEqual(donationResult.description, "Debe selecionar un estado válido para el lote.", "El error debería coincidir.");
        }

        [TestMethod]
        public void CreateDonation()
        {
            //Asigna un responsable a la donación
            DonorBM donor = create_donor();
            DonationStatusBM statusBm = get_status(1);
            DonationBLL donationBll = new DonationBLL();
            DonationBM donationBm = new DonationBM(3, donor.donorId, statusBm, "Esta es una donación creada por un test.");
            ResultBM donationResult = donationBll.SaveDonation(donationBm);

            BranchBLL branchBll = new BranchBLL();
            ResultBM branchResult = branchBll.GetBranch(1);

            
            
            donationBll.AsignResponsible(donationBm.id, voluntario);

        }

        private DonationStatusBM get_status(int id)
        {
            DonationStatusBLL statusBll = new DonationStatusBLL();
            ResultBM statusResult = statusBll.GetDonationStatus(id);
            Assert.IsTrue(statusResult.IsValid(), "El estado debería ser válido.");
            return statusResult.GetValue<DonationStatusBM>();
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
    }
}
