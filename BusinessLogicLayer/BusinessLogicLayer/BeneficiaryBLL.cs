using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAccessLayer;
using BusinessModel;

namespace BusinessLogicLayer
{
    public class BeneficiaryBLL: BLEntity
    {
        /// <summary>
        /// Recupera los datos de un beneficiario.
        /// </summary>
        /// <param name="beneficiaryId"></param>
        /// <returns></returns>
        public ResultBM GetBeneficiary(int beneficiaryId)
        {
            try
            {

                AddressBLL addressBll = new AddressBLL();
                ResultBM addressResult = null;
                BeneficiaryDAL beneficiaryDal = new BeneficiaryDAL();
                BeneficiaryBM beneficiaryBm = null;
                BeneficiaryDTO beneficiaryDto = beneficiaryDal.GetBeneficiary(beneficiaryId);

                if (beneficiaryDto != null)
                {
                    addressResult = addressBll.GetAddress(beneficiaryDto.addressId);

                    //Si hubo algún problema o la dirección no existe, entonces hay que devolver el resultado o lanzar una excepción (debería eixstir)
                    if (!addressResult.IsValid()) return addressResult;
                    if (addressResult.GetValue() == null) throw new Exception("La dirección " + beneficiaryDto.addressId + " para el beneficiario " + beneficiaryId + " no existe.");

                    beneficiaryBm = new BeneficiaryBM(beneficiaryDto, addressResult.GetValue<AddressBM>());
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", beneficiaryBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar el beneficiario " + beneficiaryId + ".", exception);
            }
        }

        /// <summary>
        /// Devuelve la lista de beneficiarios.
        /// </summary>
        /// <returns></returns>
        public ResultBM GetBeneficiaries()
        {
            try
            {
                BeneficiaryDAL beneficiaryDal = new BeneficiaryDAL();
                List<BeneficiaryDTO> beneficiaryDto = beneficiaryDal.GetBeneficiaries();
                List<BeneficiaryBM> beneficiariesBm = ConvertIntoBusinessModel(beneficiaryDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", beneficiariesBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar los donadores.", exception);
            }
        }

        /// <summary>
        /// Crea un beneficiario.
        /// </summary>
        /// <param name="beneficieryBm"></param>
        /// <returns></returns>
        public ResultBM SaveBeneficiary(BeneficiaryBM beneficieryBm)
        {
            try
            {
                BeneficiaryDAL beneficiaryDal = new BeneficiaryDAL();
                PersonBLL personBll = new PersonBLL();
                PersonBM personBm = null;
                ResultBM validationResult; //agregar validación
                ResultBM personResult;
                BeneficiaryDTO beneficiaryDto;

                // El validador no es necesario porque los datos sustanciales pertenecen a la persona
                personResult = personBll.SavePerson(beneficieryBm);
                if (!personResult.IsValid()) return personResult;

                personBm = personResult.GetValue() as PersonBM;
                beneficiaryDto = new BeneficiaryDTO(personBm.id, beneficieryBm.beneficiaryId, beneficieryBm.destination, beneficieryBm.ages, beneficieryBm.health, beneficieryBm.accessibility, beneficieryBm.majorProblem);

                beneficiaryDal.SaveBeneficiary(beneficiaryDto);
                beneficieryBm.beneficiaryId = beneficiaryDto.beneficiaryId;

                return new ResultBM(ResultBM.Type.OK, "Se ha creado el beneficiario " + beneficieryBm.name + " " + beneficieryBm.lastName + ".", beneficieryBm);


            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al crear al beneficiario.", exception);
            }
        }

        public ResultBM UpdateBeneficiary(BeneficiaryBM beneficieryBm)
        {
            try
            {
                BeneficiaryDAL beneficiaryDal = new BeneficiaryDAL();
                PersonBLL personBll = new PersonBLL();
                PersonBM personBm = null;
                ResultBM validationResult;
                ResultBM personResult;
                BeneficiaryDTO beneficiaryDto;

                // El validador no es necesario porque los datos sustanciales pertenecen a la persona
                personResult = personBll.UpdatePerson(beneficieryBm);
                if (!personResult.IsValid()) return personResult;

                personBm = personResult.GetValue() as PersonBM;
                beneficiaryDto = new BeneficiaryDTO(personBm.id, beneficieryBm.beneficiaryId, beneficieryBm.destination, beneficieryBm.ages, beneficieryBm.health, beneficieryBm.accessibility, beneficieryBm.majorProblem);

                beneficiaryDal.UpdateDonor(beneficiaryDto);

                return new ResultBM(ResultBM.Type.OK, "Se ha actualizado el beneficiario " + beneficieryBm.name + " " + beneficieryBm.lastName + ".", beneficieryBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al crear al donador.", exception);
            }
        }

        private List<BeneficiaryBM> ConvertIntoBusinessModel(List<BeneficiaryDTO> beneficiaries)
        {
            List<BeneficiaryBM> result = new List<BeneficiaryBM>();
            foreach (BeneficiaryDTO beneficiary in beneficiaries)
            {
                result.Add(new BeneficiaryBM(beneficiary, GetAddress(beneficiary)));
            }
            return result;
        }

        //No está bueno esto, pero me permite recuperar las direcciones. Poco performante... pero no hay tiempo.
        private AddressBM GetAddress(BeneficiaryDTO beneficiary)
        {
            ResultBM addressResult = new AddressBLL().GetAddress(beneficiary.addressId);
            return addressResult.GetValue<AddressBM>();
        }

        public ResultBM GetCollection(Dictionary<string, string> filter = null)
        {
            return GetBeneficiaries();
        }

        public ResultBM Delete(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
