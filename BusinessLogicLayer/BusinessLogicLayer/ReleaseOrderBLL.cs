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
                    if (beneficiaryResult.GetValue() == null)  throw new Exception("El beneficiario " + releaseOrderDto.beneficiaryId + " para la order de salida " + releaseOrderId + " no existe.");

                    releaseOrderDetailResult = releaseOrderDetailBll.GetReleaseOrderDetail(releaseOrderId);
                    if (!releaseOrderDetailResult.IsValid()) return releaseOrderDetailResult;
                    if (releaseOrderDetailResult.GetValue() == null) throw new Exception("El detalle para la order de salida " + releaseOrderId + " no existe.");
                }

                releaseOrderBm = new ReleaseOrderBM(releaseOrderDto, beneficiaryResult.GetValue<BeneficiaryBM>(), releaseOrderDetailResult.GetValue<List<ReleaseOrderDetailBM>>());
                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", releaseOrderBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar los países.", exception);
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
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al guardar la orden de salida.", exception);
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
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al actualizar la orden de salida.", exception);
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
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar las órdenes de salida.", exception);
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
            //if (addressBm.street == null || addressBm.street.Length == 0)
            //    return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe completarse la dirección");
            
            return new ResultBM(ResultBM.Type.OK);
        }

        public ResultBM GetCollection(Dictionary<string, string> filter = null)
        {
            return GetReleaseOrders();
        }

        public ResultBM Delete(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
