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
    public class DonationBLL
    {
        public ResultBM GetDonation(int donationId)
        {
            try
            {
                DonationStatusBLL donationStatusBll = new DonationStatusBLL();
                ResultBM statusResult = null;
                DonationDAL donationDal = new DonationDAL();                
                DonationDTO donationDto = donationDal.GetDonation(donationId);
                DonationBM donationBm = null;

                //Si la donación existe, debería existir el estado
                if (donationDto != null)
                {
                    statusResult = donationStatusBll.GetDonationStatus(donationDto.statusId);

                    if (statusResult.IsValid())
                    {
                        if (statusResult.GetValue() != null)
                            donationBm = new DonationBM(donationDto, statusResult.GetValue<DonationStatusBM>());
                        else
                            throw new Exception("El estado con id " + donationDto.statusId + "para la donación " + donationId + " no existe.");
                    }
                    else
                        return statusResult;
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", donationBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar la donación " + donationId + ".", exception);
            }
        }

        public ResultBM SaveDonation(DonationBM donationBm)
        {
            try
            {
                DonationDAL donationDal = new DonationDAL();
                DonationDTO donationDto;
                ResultBM validationResult = IsValid(donationBm);

                if (validationResult.IsValid())
                {
                    donationDto = new DonationDTO(donationBm.items, donationBm.arrival, donationBm.donationStatus.id, donationBm.donorId, donationBm.comment, donationBm.volunteerId);
                    donationDal.SaveDonor(donationDto);
                    donationBm.id = donationDto.id;

                    return new ResultBM(ResultBM.Type.OK, "Se ha creado la donación.", donationBm);
                    
                }
                else
                {
                    return validationResult;
                }
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al crear al donador.", exception);
            }
        }

        private ResultBM IsValid(DonationBM donationBm)
        {
            if (donationBm.items < 1)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "La cantidad de bultos debe ser de al menos una unidad.");

            if (donationBm.donationStatus == null)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe selecionar un estado válido para el lote.");

            if (donationBm.donorId == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe asignarse donador.");
            
            return new ResultBM(ResultBM.Type.OK);
        }

    }
}
