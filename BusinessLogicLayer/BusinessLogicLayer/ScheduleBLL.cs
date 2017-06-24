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
    public class ScheduleBLL
    {
        public ResultBM GetSchedule(int scheduleId)
        {
            try
            {
                DonationBLL donationBll = new DonationBLL();
                ResultBM donationResult = null;
                ScheduleDAL scheduleDal = new ScheduleDAL();
                ScheduleDTO scheduleDto = scheduleDal.GetSchedule(scheduleId);
                ScheduleBM scheduleBm = null;

                // Si la agenda existe, entonces debería existir la donación.
                if (scheduleDto != null)
                {
                    donationResult = donationBll.GetDonation(scheduleDto.id);

                    if (!donationResult.IsValid()) return donationResult;
                    if (donationResult.GetValue() == null) throw new Exception("La donación " + scheduleDto.donationId + " para la la agenda " + scheduleId+ " no existe.");

                    scheduleBm = new ScheduleBM(scheduleDto, donationResult.GetValue<DonationBM>());
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", scheduleBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar la agenda " + scheduleId + ".", exception);
            }
        }

        public ResultBM SaveSchedule(ScheduleBM scheduleBm)
        {
            try
            {
                ScheduleDAL scheduleDal = new ScheduleDAL();
                ScheduleDTO scheduleDto = null;
                ResultBM validResult = IsValid(scheduleBm);

                if (!validResult.IsValid()) return validResult;
                scheduleDto = new ScheduleDTO(scheduleBm.visit, scheduleBm.donation.id, scheduleBm.comment);
                scheduleDal.SaveSchedule(scheduleDto);
                scheduleBm.id = scheduleDto.id;

                return new ResultBM(ResultBM.Type.OK, "Agenda guardada.", scheduleBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al guardar la dirección.", exception);
            }
        }

        public ResultBM GetSchedules()
        {
            try
            {
                ScheduleDAL scheduleDal = new ScheduleDAL();
                List<ScheduleDTO> donationsDto = scheduleDal.GetSchedules();
                List<ScheduleBM> donationsBm = ConvertIntoBusinessModel(donationsDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", donationsBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar las agendas.", exception);
            }
        }

        private List<ScheduleBM> ConvertIntoBusinessModel(List<ScheduleDTO> schedules)
        {
            List<ScheduleBM> result = new List<ScheduleBM>();
            foreach (ScheduleDTO schedule in schedules)
            {
                result.Add(new ScheduleBM(schedule, GetDonation(schedule)));
            }
            return result;
        }

        //No está bueno esto, pero me permite recuperar el voluntario. Poco performante... pero no hay tiempo.
        private DonationBM GetDonation(ScheduleDTO schedule)
        {
            ResultBM statusResult = new DonationBLL().GetDonation(schedule.donationId);
            return statusResult.GetValue<DonationBM>();
        }

        private ResultBM IsValid(ScheduleBM scheduleBm)
        {
            return new ResultBM(ResultBM.Type.OK);
        }
    }
}
