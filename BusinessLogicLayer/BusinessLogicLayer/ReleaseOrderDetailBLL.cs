using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel;
using DataTransferObject;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class ReleaseOrderDetailBLL
    {
        public ResultBM GetReleaseOrderDetail(int releaseOrderId)
        {
            try
            {

                ReleaseOrderDetailDAL detailDAL = new ReleaseOrderDetailDAL();
                List<ReleaseOrderDetailDTO> detailDto = detailDAL.GetReleaseOrderDetail(releaseOrderId);
                List<ReleaseOrderDetailBM> detailBms = ConvertIntoBusinessModel(detailDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", detailBms);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar los países.", exception);
            }
        }

        public ResultBM SaveReleaseOrderDetail(ReleaseOrderBM releaseOrderBm)
        {
            try
            {
                ReleaseOrderDetailDAL detailDal = new ReleaseOrderDetailDAL();
                ReleaseOrderDetailDTO detailDto = null;
                List<ReleaseOrderDetailDTO> listDetail = new List<ReleaseOrderDetailDTO>();
                ResultBM validResult = IsValid(releaseOrderBm.detail);

                if (!validResult.IsValid()) return validResult;

                foreach (ReleaseOrderDetailBM detail in releaseOrderBm.detail)
                {
                    detailDto = new ReleaseOrderDetailDTO();
                    listDetail.Add(detailDto);
                }
                detailDal.SaveReleaseOrderDetail(listDetail);

                return new ResultBM(ResultBM.Type.OK, "Detalle guardado para la orden " + releaseOrderBm.id + ".", releaseOrderBm.detail);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al guardar el detalle.", exception);
            }
        }

        public ResultBM UpdateReleaseOrderDetail(ReleaseOrderBM releaseOrderBm)
        {
            try {
                ReleaseOrderDetailDAL detailDal = new ReleaseOrderDetailDAL();
                detailDal.DeleteReleaseOrderDetail(releaseOrderBm.id);
                return SaveReleaseOrderDetail(releaseOrderBm);
            }
            catch (Exception exception) {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al actualizar el detalle.", exception);
            }
            
        }

        private List<ReleaseOrderDetailBM> ConvertIntoBusinessModel(List<ReleaseOrderDetailDTO> details)
        {
            List<ReleaseOrderDetailBM> result = new List<ReleaseOrderDetailBM>();
            foreach (ReleaseOrderDetailDTO detail in details)
            {
                result.Add(new ReleaseOrderDetailBM(detail));
            }
            return result;
        }

        private ResultBM IsValid(List<ReleaseOrderDetailBM> releaseOrderDetail)
        {
            //if (addressBm.street == null || addressBm.street.Length == 0)
            //    return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe completarse la dirección");

            //if (addressBm.number < 0)
            //    return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe completarse el número de calle");

            //if (addressBm.country == null)
            //    return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe completarse el país");

            return new ResultBM(ResultBM.Type.OK);
        }
    }
}
