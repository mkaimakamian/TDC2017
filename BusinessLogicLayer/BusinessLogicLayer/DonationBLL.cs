using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAccessLayer;
using BusinessModel;
using Helper;

namespace BusinessLogicLayer
{
    public class DonationBLL: BLEntity
    {
        public ResultBM GetDonation(int donationId)
        {
            try
            {
                VolunteerBLL volunteerBll = new VolunteerBLL();
                ResultBM volunteerResult = null;
                VolunteerBM volunteerBm = null;
                DonationStatusBLL donationStatusBll = new DonationStatusBLL();
                ResultBM statusResult = null;
                DonorBLL donorBll = new DonorBLL();
                ResultBM donorResult = null;

                DonationDAL donationDal = new DonationDAL();
                DonationBM donationBm = null;
                DonationDTO donationDto = donationDal.GetDonation(donationId);

                //Si la donación existe, debería existir el estado
                if (donationDto != null)
                {
                    statusResult = donationStatusBll.GetDonationStatus(donationDto.statusId);
                    if (!statusResult.IsValid()) return statusResult;
                    if (statusResult.GetValue() == null) throw new Exception(SessionHelper.GetTranslation("RETRIEVING_ERROR") + " statusId " + donationDto.statusId);

                    donorResult = donorBll.GetDonor(donationDto.donorId);
                    if (!donorResult.IsValid()) return donorResult;
                    if (donorResult.GetValue() == null) throw new Exception(SessionHelper.GetTranslation("RETRIEVING_ERROR") + " donorId " + donationDto.donorId);

                    //Podría no existir voluntario, sobre todo si se consulta una donación recién creada
                    volunteerResult = volunteerBll.GetVolunteer(donationDto.volunteerId);
                    if (volunteerResult.GetValue() != null) volunteerBm = volunteerResult.GetValue<VolunteerBM>();

                    donationBm = new DonationBM(donationDto, donorResult.GetValue<DonorBM>(), statusResult.GetValue<DonationStatusBM>(), volunteerBm);
                    
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", donationBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM GetDonations()
        {
            try {
                DonationDAL donationDal = new DonationDAL();
                List<DonationDTO> donationsDto = donationDal.GetDonations();
                List<DonationBM> donationsBm = ConvertIntoBusinessModel(donationsDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", donationsBm);
            }
            catch (Exception exception) {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM GetAvaliableDonations()
        {
            try {
                DonationDAL donationDal = new DonationDAL();
                List<DonationDTO> donationsDto = donationDal.GetAvaliableDonations();
                List<DonationBM> donationsBm = ConvertIntoBusinessModel(donationsDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", donationsBm);
            }
            catch (Exception exception) {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM SaveDonation(DonationBM donationBm)
        {
            try
            {
                DonationDAL donationDal = new DonationDAL();
                DonationDTO donationDto;
                ResultBM validationResult = IsValid(donationBm);

                if (!validationResult.IsValid()) return validationResult;
                donationDto = new DonationDTO(donationBm.Items, donationBm.Arrival, donationBm.donationStatus.id, donationBm.donor.donorId, donationBm.Comment, donationBm.volunteer.volunteerId);
                donationDal.SaveDonation(donationDto);
                donationBm.id = donationDto.id;

                return new ResultBM(ResultBM.Type.OK, "Se ha creado la donación.", donationBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("SAVING_ERROR") + " " + exception.Message, exception);
            }
        }
                
        public ResultBM UpdateDonation(DonationBM donationBm)
        {
            try
            {
                DonationDAL donationDal = new DonationDAL();
                DonationDTO donationDto;
                ResultBM validationResult = IsValid(donationBm);

                if (!validationResult.IsValid()) return validationResult;
                donationDto = new DonationDTO(donationBm.Items, donationBm.Arrival, donationBm.donationStatus.id, donationBm.donor.donorId, donationBm.Comment, donationBm.volunteer == null ? 0 : donationBm.volunteer.volunteerId, donationBm.id);
                donationDal.UpdateDonation(donationDto);

                return new ResultBM(ResultBM.Type.OK, "Se ha actualizado la donación.", donationBm);
                
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("UPDATING_ERROR") + " " + exception.Message, exception);
            }
        }

        public void UpdateToStoredStatusIfApply(int id)
        {
            DonationDAL donationDal = new DonationDAL();
            donationDal.UpdateStatusToStored(id, (int) DonationStatusBM.Status.STORED);
        }

        public void UpdateToReceivedStatus(int id)
        {
            DonationDAL donationDal = new DonationDAL();
            donationDal.UpdateToStatus(id, (int)DonationStatusBM.Status.RECEIVED);
        }

        private List<DonationBM> ConvertIntoBusinessModel(List<DonationDTO> donations)
        {
            List<DonationBM> result = new List<DonationBM>();
            foreach (DonationDTO donation in donations)
            {
                result.Add(new DonationBM(donation, GetDonor(donation), GetStatus(donation), GetVolunteer(donation)));
            }
            return result;
        }

        //No está bueno esto, pero me permite recuperar el voluntario. Poco performante... pero no hay tiempo.
        private VolunteerBM GetVolunteer(DonationDTO donationDto)
        {
            if (donationDto.volunteerId > 0)
            {
                ResultBM volunteerResult = new VolunteerBLL().GetVolunteer(donationDto.volunteerId);
                return volunteerResult.GetValue<VolunteerBM>();
            }
            else
                return null;
        }

        //No está bueno esto, pero me permite recuperar el estado. Poco performante... pero no hay tiempo.
        private DonationStatusBM GetStatus(DonationDTO donationDto)
        {
            ResultBM statusResult = new DonationStatusBLL().GetDonationStatus(donationDto.statusId);
            return statusResult.GetValue<DonationStatusBM>();
        }

        //No está bueno esto, pero me permite recuperar el donador. Poco performante... pero no hay tiempo.
        private DonorBM GetDonor(DonationDTO donationDto)
        {
            ResultBM statusResult = new DonorBLL().GetDonor(donationDto.donorId);
            return statusResult.GetValue<DonorBM>();
        }

        private ResultBM IsValid(DonationBM donationBm)
        {
            if (donationBm.Items < 1)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("INVALID_VALUE_ERROR") + " (<1)");

            if (donationBm.donationStatus == null)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (STATUS)");

            if (donationBm.donor == null || donationBm.donor.donorId == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (DONOR)");
            
            return new ResultBM(ResultBM.Type.OK);
        }

        public ResultBM Delete(object entity)
        {
            try
            {
                DonationDAL donationDal = new DonationDAL();
                DonationBM donationBm = entity as DonationBM;

                if (donationBm.IsReceived())
                {
                    if (!donationDal.IsInUse(donationBm.id))
                    {
                        donationDal.DeleteDonation(donationBm.id);
                        return new ResultBM(ResultBM.Type.OK, "Se ha eliminado el registro.", donationBm);
                    }
                    else
                    {
                        return new ResultBM(ResultBM.Type.FAIL, SessionHelper.GetTranslation("DONATION_ASSIG_UNDELETEABLE_ERROR"), donationBm);
                    }

                }
                else
                {
                    return new ResultBM(ResultBM.Type.FAIL, SessionHelper.GetTranslation("DONATION_STATUS_UNDELETEABLE_ERROR"), donationBm);
                }
                
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("DELETING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM GetCollection(Dictionary<string, string> filter = null)
        {
            return GetDonations();
        }
    }
}
