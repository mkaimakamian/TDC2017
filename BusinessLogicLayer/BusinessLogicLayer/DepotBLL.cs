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
    public class DepotBLL
    {
        public ResultBM GetDepot(int depotId)
        {
            try
            {

                AddressBLL addressBll = new AddressBLL();
                ResultBM addressResult = null;
                DepotDAL depotDal = new DepotDAL();
                DepotDTO depotDto = depotDal.GetDepot(depotId);
                DepotBM depotBm = null;

                //Si existe el depósito debe existir la dirección
                if (depotDto != null)
                {
                    addressResult = addressBll.GetAddress(depotDto.addressId);
                    if (!addressResult.IsValid()) return addressResult;
                    if (addressResult.GetValue() == null) throw new Exception("La dirección " + depotDto.addressId + " para el depósito " + depotId + " no existe.");

                    depotBm = new DepotBM(depotDto, addressResult.GetValue<AddressBM>());
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", depotBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar el depósito " + depotId + ".", exception);
            }
        }

        /// <summary>
        /// Recupera todos los depósitos pero sin las direcciones
        /// </summary>
        /// <returns></returns>
        public ResultBM GetDepots()
        {
            try
            {
                DepotDAL depotDal = new DepotDAL();
                List<DepotDTO> depotsDto = depotDal.GetDepots();
                List<DepotBM> depotsBm = ConvertIntoBusinessModel(depotsDto);

                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", depotsBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar los países.", exception);
            }
        }

        private List<DepotBM> ConvertIntoBusinessModel(List<DepotDTO> depots)
        {
            List<DepotBM> result = new List<DepotBM>();
            foreach (DepotDTO depot in depots)
            {
                result.Add(new DepotBM(depot));
            }
            return result;
        }
    }
}
