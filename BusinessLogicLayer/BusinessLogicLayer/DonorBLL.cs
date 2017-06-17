using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTransferObject;
using BusinessModel;

namespace BusinessLogicLayer
{
    public class DonorBLL: BLEntity
    {

        public ResultBM GetDonor(int donorId)
        {
            try
            {

                AddressBLL addressBll = new AddressBLL();
                ResultBM addressResult = null;
                OrganizationBLL organizationBll = new OrganizationBLL();
                OrganizationBM organizationBm = null;
                ResultBM resultOrganization = null;
                DonorDAL donorDal = new DonorDAL();
                DonorBM donorBm = null;

                DonorDTO donorDto = donorDal.GetDonor(donorId);

                // Si el donador existe, deb existir la persona
                if (donorDto != null)
                {
                    addressResult = addressBll.GetAddress(donorDto.addressId);

                    //Si hubo algún problema o la dirección no existe, entonces hay que devolver el resultado o lanzar una excepción (debería eixstir)
                    if (!addressResult.IsValid()) return addressResult;
                    if(addressResult.GetValue() == null) throw new Exception("La persona " + donorDto.donorId + " para el donador " + donorId + " no existe.");

                    // Podría no pertenecer a una organización, de modo tal que si no posee relación, está bien
                    if (donorDto.organizationId != 0)
                    {
                        resultOrganization = organizationBll.GetOrganization(donorDto.organizationId);

                        if (!resultOrganization.IsValid()) return resultOrganization;
                        if (resultOrganization.GetValue() == null) throw new Exception("La persona " + donorDto.donorId + " para el donador " + donorId + " no existe.");
                        
                        organizationBm = resultOrganization.GetValue<OrganizationBM>();
                    }

                    donorBm = new DonorBM(donorDto, addressResult.GetValue<AddressBM>(), organizationBm);
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", donorBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar el donador " + donorId + ".", exception);
            }
        }

        /// <summary>
        /// Devuelve la lista de donadores
        /// </summary>
        /// <returns></returns>
        public ResultBM GetDonors()
        {
            try {
                DonorDAL donorDal = new DonorDAL();
                List<DonorDTO> donorsDto = donorDal.GetDonors();
                List<DonorBM> donorsBm = ConvertIntoBusinessModel(donorsDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", donorsBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar los donadores.", exception);
            }
        }

        /// <summary>
        /// Crea un nuevo donador.
        /// </summary>
        /// <param name="donorBm"></param>
        /// <returns></returns>
        public ResultBM SaveDonor(DonorBM donorBm)
        {
            try
            {
                OrganizationBLL organizationBll = new OrganizationBLL();
                ResultBM resultOrganization = null;
                OrganizationBM organizationBm = null;
                DonorDAL donorDal = new DonorDAL();
                PersonBLL personBll = new PersonBLL();
                PersonBM personBm = null;
                ResultBM validationResult;
                ResultBM personResult;
                DonorDTO donorDto;
                
                // El validador no es necesario porque el donador es una combinación de otras entidades que ya poseen validadores.
                validationResult = IsValid(donorBm);
                if (!validationResult.IsValid()) return validationResult;

                personResult = personBll.SavePerson(donorBm);
                if (!personResult.IsValid()) return personResult;

                if (donorBm.organization != null)
                {
                    resultOrganization = organizationBll.SaveOrganization(donorBm.organization);
                    if (!resultOrganization.IsValid()) return resultOrganization;
                    if (resultOrganization.GetValue() == null) return new ResultBM(ResultBM.Type.FAIL, "Se ha producido un error al guardar la organización del donador.", resultOrganization);
                    organizationBm = resultOrganization.GetValue<OrganizationBM>();
                }

                personBm = personResult.GetValue() as PersonBM;
                donorDto = new DonorDTO(personBm.id, organizationBm == null ? 0 : organizationBm.id, donorBm.canBeContacted);
                donorDal.SaveDonor(donorDto);
                donorBm.donorId = donorDto.donorId;

                return new ResultBM(ResultBM.Type.OK, "Se ha creado el donador " + donorBm.Name + " " + donorBm.LastName + ".", donorBm);
                   
               
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al crear al donador.", exception);
            }
        }

        public ResultBM UpdateDonor(DonorBM donorBm)
        {
            try
            {
                DonorDAL donorDal = new DonorDAL();
                DonorDTO donorDto;
                ResultBM validationResult = IsValid(donorBm);

                if (validationResult.IsValid())
                {
                    //TODO COMPLETAR:
                    //1. actualizar usuario
                    //2. actualizar donador
                    //donorDto = new DonorDTO(donationBm.items, donationBm.arrival, donationBm.donationStatus.id, donationBm.donorId, donationBm.comment, donationBm.volunteer == null ? 0 : donationBm.volunteer.volunteerId, donationBm.id);
                    //donationDal.UpdateDonation(donationDto);

                    return new ResultBM(ResultBM.Type.OK, "Se ha actualizado la donación.", donorBm);

                }
                else
                {
                    return validationResult;
                }
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al actualizar la donación.", exception);
            }
        }

        private List<DonorBM> ConvertIntoBusinessModel(List<DonorDTO> donors)
        {
            List<DonorBM> result = new List<DonorBM>();
            foreach (DonorDTO donor in donors)
            {
                result.Add(new DonorBM(donor));
            }
            return result;
        }

        public ResultBM IsValid(DonorBM donorBm)
        {
            //Un donador es una persona con un conjunto de valores cuyas entidades base se encargan de validar lo que haga falta
            return new ResultBM(ResultBM.Type.OK);
        }

        public ResultBM GetCollection()
        {
            return GetDonors();
        }

        public ResultBM Delete(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
