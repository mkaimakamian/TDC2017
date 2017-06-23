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
                DonationDAL donationDal = new DonationDAL();                
                DonationBM donationBm = null;
                DonationDTO donationDto = donationDal.GetDonation(donationId);

                //Si la donación existe, debería existir el estado
                if (donationDto != null)
                {
                    statusResult = donationStatusBll.GetDonationStatus(donationDto.statusId);

                    if (!statusResult.IsValid()) return statusResult;
                    if (statusResult.GetValue() == null) throw new Exception("El estado con id " + donationDto.statusId + "para la donación " + donationId + " no existe.");
                    //Podría no existir voluntario, sobre todo si se consulta una donación recién creada
                    volunteerResult = volunteerBll.GetVolunteer(donationDto.volunteerId);
                    if (volunteerResult.GetValue() != null) volunteerBm = volunteerResult.GetValue<VolunteerBM>();

                    donationBm = new DonationBM(donationDto, statusResult.GetValue<DonationStatusBM>(), volunteerBm);
                    
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", donationBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar la donación " + donationId + ".", exception);
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
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar las donaciones.", exception);
            }
        }

        //debería filtrar por: stocked != 0 o por status != stocked (si es que existe ese valor)
        public ResultBM GetAvaliableDonations()
        {
            try {
                DonationDAL donationDal = new DonationDAL();
                List<DonationDTO> donationsDto = donationDal.GetAvaliableDonations();
                List<DonationBM> donationsBm = ConvertIntoBusinessModel(donationsDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", donationsBm);
            }
            catch (Exception exception) {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar las donaciones.", exception);
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
                donationDto = new DonationDTO(donationBm.Items, donationBm.Arrival, donationBm.donationStatus.id, donationBm.donorId, donationBm.Comment, 0);
                donationDal.SaveDonation(donationDto);
                donationBm.id = donationDto.id;

                return new ResultBM(ResultBM.Type.OK, "Se ha creado la donación.", donationBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al crear la donación.", exception);
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
                donationDto = new DonationDTO(donationBm.Items, donationBm.Arrival, donationBm.donationStatus.id, donationBm.donorId, donationBm.Comment, donationBm.volunteer == null ? 0 : donationBm.volunteer.volunteerId, donationBm.id);
                donationDal.UpdateDonation(donationDto);

                return new ResultBM(ResultBM.Type.OK, "Se ha actualizado la donación.", donationBm);
                
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al actualizar la donación.", exception);
            }
        }

        private List<DonationBM> ConvertIntoBusinessModel(List<DonationDTO> donations)
        {
            List<DonationBM> result = new List<DonationBM>();
            foreach (DonationDTO donation in donations)
            {
                result.Add(new DonationBM(donation, GetStatus(donation), GetVolunteer(donation)));
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

        //No está bueno esto, pero me permite recuperar el voluntario. Poco performante... pero no hay tiempo.
        private DonationStatusBM GetStatus(DonationDTO donationDto)
        {
            ResultBM statusResult = new DonationStatusBLL().GetDonationStatus(donationDto.statusId);
            return statusResult.GetValue<DonationStatusBM>();
        }

        private ResultBM IsValid(DonationBM donationBm)
        {
            if (donationBm.Items < 1)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "La cantidad de bultos debe ser de al menos una unidad.");

            if (donationBm.donationStatus == null)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe selecionar un estado válido para el lote.");

            if (donationBm.donorId == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe asignarse donador.");
            
            return new ResultBM(ResultBM.Type.OK);
        }


        public ResultBM GetCollection()
        {
            return GetDonations();
        }

        public ResultBM Delete(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
