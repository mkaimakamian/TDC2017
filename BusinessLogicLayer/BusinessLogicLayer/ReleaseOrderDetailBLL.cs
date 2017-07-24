using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel;
using DataTransferObject;
using DataAccessLayer;
using Helper;

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
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
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
                    detailDto.releaseOrderId = releaseOrderBm.id;
                    detailDto.quantity = detail.Quantity;
                    detailDto.stockId = detail.stock.id;
                    listDetail.Add(detailDto);
                }
                detailDal.SaveReleaseOrderDetail(listDetail);

                return new ResultBM(ResultBM.Type.OK, "Detalle guardado para la orden " + releaseOrderBm.id + ".", releaseOrderBm.detail);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("SAVING_ERROR") + " " + exception.Message, exception);
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
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("UPDATING_ERROR") + " " + exception.Message, exception);
            }
            
        }

        private List<ReleaseOrderDetailBM> ConvertIntoBusinessModel(List<ReleaseOrderDetailDTO> details)
        {
            List<ReleaseOrderDetailBM> result = new List<ReleaseOrderDetailBM>();
            foreach (ReleaseOrderDetailDTO detail in details)
            {
                result.Add(new ReleaseOrderDetailBM(detail, GetStock(detail)));
            }
            return result;
        }

        //No está bueno esto, pero me permite recuperar las direcciones. Poco performante... pero no hay tiempo.
        private StockBM GetStock(ReleaseOrderDetailDTO detail)
        {
            ResultBM stockResult = new StockBLL().GetStock(detail.stockId);
            return stockResult.GetValue<StockBM>();
        }

        private ResultBM IsValid(List<ReleaseOrderDetailBM> releaseOrderDetail)
        {
            return new ResultBM(ResultBM.Type.OK);
        }
    }
}
