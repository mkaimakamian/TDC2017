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
    public class ReleaseOrderBLL : BLEntity
    {

        public ResultBM GetReleaseOrder(int releaseOrderId)
        {
            try
            {
                BeneficiaryBLL beneficiaryBll = new BeneficiaryBLL();
                ResultBM beneficiaryResult = null;
                ReleaseOrderDAL releaseOrderDal = new ReleaseOrderDAL();
                ReleaseOrderDTO releaseOrderDto = releaseOrderDal.GetReleaseOrder(releaseOrderId);
                ReleaseOrderBM releaseOrderBm = null;
                ReleaseOrderDetailBLL releaseOrderDetailBll = new ReleaseOrderDetailBLL();
                ResultBM releaseOrderDetailResult = null;

                //Si la orden de salida existe, deben existir beneficiario y detalles
                if (releaseOrderDto != null)
                {
                    beneficiaryResult = beneficiaryBll.GetBeneficiary(releaseOrderDto.beneficiaryId);
                    if (!beneficiaryResult.IsValid()) return beneficiaryResult;
                    if (beneficiaryResult.GetValue() == null) throw new Exception(SessionHelper.GetTranslation("RETRIEVING_ERROR") + " beneficiaryId " + releaseOrderDto.beneficiaryId);

                    releaseOrderDetailResult = releaseOrderDetailBll.GetReleaseOrderDetail(releaseOrderId);
                    if (!releaseOrderDetailResult.IsValid()) return releaseOrderDetailResult;
                    if (releaseOrderDetailResult.GetValue() == null) throw new Exception(SessionHelper.GetTranslation("RETRIEVING_ERROR") + " releaseOrderId " + releaseOrderId);
                }

                releaseOrderBm = new ReleaseOrderBM(releaseOrderDto, beneficiaryResult.GetValue<BeneficiaryBM>(), releaseOrderDetailResult.GetValue<List<ReleaseOrderDetailBM>>());
                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", releaseOrderBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM SaveReleaseOrder(ReleaseOrderBM releaseOrderBm)
        {
            try
            {
                ReleaseOrderDetailBLL releaseOrderDetailBll = new ReleaseOrderDetailBLL();
                ResultBM detailResult = null;
                ReleaseOrderDAL releaseOrderDal = new ReleaseOrderDAL();
                ReleaseOrderDTO releaseOrderDto = null;
                ResultBM validResult = IsValid(releaseOrderBm);

                if (!validResult.IsValid()) return validResult;
                releaseOrderDto = new ReleaseOrderDTO(releaseOrderBm.id, releaseOrderBm.beneficiary.beneficiaryId, releaseOrderBm.Comment, releaseOrderBm.released, releaseOrderBm.received, releaseOrderBm.OrderStatus);
                releaseOrderDal.SaveReleaseOrder(releaseOrderDto);
                releaseOrderBm.id = releaseOrderDto.id;

                detailResult = releaseOrderDetailBll.SaveReleaseOrderDetail(releaseOrderBm);
                if (!detailResult.IsValid()) return detailResult;

                return new ResultBM(ResultBM.Type.OK, "Orden de salida guardada.", releaseOrderBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("SAVING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM UpdateReleaseOrder(ReleaseOrderBM releaseOrderBm)
        {
            try
            {
                ReleaseOrderDetailBLL releaseOrderDetailBll = new ReleaseOrderDetailBLL();
                ResultBM detailResult = null;
                ReleaseOrderDAL releaseOrderDal = new ReleaseOrderDAL();
                ReleaseOrderDTO releaseOrderDto = null;
                ResultBM validResult = IsValid(releaseOrderBm);

                if (!validResult.IsValid()) return validResult;
                releaseOrderDto = new ReleaseOrderDTO(releaseOrderBm.id, releaseOrderBm.beneficiary.beneficiaryId, releaseOrderBm.Comment, releaseOrderBm.released, releaseOrderBm.received, releaseOrderBm.OrderStatus);
                releaseOrderDal.UpdateReleaseOrder(releaseOrderDto);

                detailResult = releaseOrderDetailBll.UpdateReleaseOrderDetail(releaseOrderBm);
                if (!detailResult.IsValid()) return detailResult;

                return new ResultBM(ResultBM.Type.OK, "Orden de salida actualizada.", releaseOrderBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("UPDATING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM GetReleaseOrders()
        {
            try
            {

                ReleaseOrderDAL releaseOrderDal = new ReleaseOrderDAL();
                List<ReleaseOrderDTO> releaseOrderDtos = releaseOrderDal.GetReleaseOrders();
                List<ReleaseOrderBM> releaseOrderBms = ConvertIntoBusinessModel(releaseOrderDtos);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", releaseOrderBms);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        private List<ReleaseOrderBM> ConvertIntoBusinessModel(List<ReleaseOrderDTO> orders)
        {
            List<ReleaseOrderBM> result = new List<ReleaseOrderBM>();
            foreach (ReleaseOrderDTO order in orders)
            {
                result.Add(new ReleaseOrderBM(order, GetBeneficiary(order), GetDetail(order)));
            }
            return result;
        }

        //No está bueno esto, pero me permite recuperar los beneficiarios. Poco performante... pero no hay tiempo.
        private BeneficiaryBM GetBeneficiary(ReleaseOrderDTO releaseOrderDto)
        {
            ResultBM beneficiaryResult = new BeneficiaryBLL().GetBeneficiary(releaseOrderDto.beneficiaryId);
            return beneficiaryResult.GetValue<BeneficiaryBM>();
        }

        private List<ReleaseOrderDetailBM> GetDetail(ReleaseOrderDTO releaseOrderDto)
        {
            ResultBM detailResult = new ReleaseOrderDetailBLL().GetReleaseOrderDetail(releaseOrderDto.id);
            return detailResult.GetValue<List<ReleaseOrderDetailBM>>();
        }

        private ResultBM IsValid(ReleaseOrderBM releaseOrder)
        {
            if (releaseOrder.detail == null || releaseOrder.detail.Count == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (NO ITEMS)");

            return new ResultBM(ResultBM.Type.OK);
        }

        public ResultBM GetCollection(Dictionary<string, string> filter = null)
        {
            return GetReleaseOrders();
        }

        public ResultBM Delete(object entity)
        {
            try
            {
                ReleaseOrderDAL releaseOrderDal = new ReleaseOrderDAL();
                ReleaseOrderBM releaseOrderBm = entity as ReleaseOrderBM;

                if (releaseOrderBm.OrderStatus == ReleaseOrderBM.Status.PENDING.ToString())
                {
                    releaseOrderDal.DeleteReleaseOrder(releaseOrderBm.id);
                    return new ResultBM(ResultBM.Type.OK, "Se ha eliminado el registro.", releaseOrderBm);
                }
                else
                {
                    return new ResultBM(ResultBM.Type.FAIL, SessionHelper.GetTranslation("RO_UNDELETEABLE_ERROR"), releaseOrderBm);
                }

            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("DELETING_ERROR") + " " + exception.Message, exception);
            }
        }
    }
}
