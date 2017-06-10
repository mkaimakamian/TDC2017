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
    public class DonationStatusBLL
    {
        /// <summary>
        /// Recupera el status de la donación segun el id informado por parámetro.
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public ResultBM GetDonationStatus(int statusId)
        {
            try
            {
                DonationStatusDAL donationStatusDal = new DonationStatusDAL();
                DonationStatusDTO donationStatusDto = donationStatusDal.GetDonationStatus(statusId);
                DonationStatusBM donationStatusBm = null;

                if (donationStatusDto != null)
                    donationStatusBm = new DonationStatusBM(donationStatusDto);

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", donationStatusBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar el estado " + statusId+ ".", exception);
            }
        }
    }
}
