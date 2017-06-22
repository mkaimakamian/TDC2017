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
    public class StockBLL: BLEntity
    {

        public ResultBM GetStock(int stockId)
        {
            try {
                DonationBLL donationBll = new DonationBLL();
                ResultBM donationResult = null;
                DepotBLL depotBll = new DepotBLL();
                ResultBM depotResult = null;
                ItemTypeBLL itemTypeBll = new ItemTypeBLL();
                ResultBM itemTypeResult = null;
                StockDAL stockDal = new StockDAL();
                StockDTO stockDto = stockDal.GetStock(stockId);
                StockBM stockBm = null;

                //Si existe el stock, las relaciones deberían existir... TODAS
                if (stockDto != null)
                {
                    donationResult = donationBll.GetDonation(stockDto.donationId);
                    if (!donationResult.IsValid()) return donationResult;
                    if (donationResult.GetValue() == null) throw new Exception("La donación " + stockDto.donationId + " para el stock " + stockId + " no existe.");

                    depotResult = depotBll.GetDepot(stockDto.depotId);
                    if (!depotResult.IsValid()) return depotResult;
                    if (depotResult.GetValue() == null) throw new Exception("El depósito " + stockDto.depotId + " para el stock " + stockId + " no existe.");

                    itemTypeResult = itemTypeBll.GetItemType(stockDto.itemTypeId);
                    if (!itemTypeResult.IsValid()) return itemTypeResult;
                    if (itemTypeResult.GetValue() == null) throw new Exception("El tipo de artículo " + stockDto.itemTypeId + " para el stock " + stockId + " no existe.");

                    stockBm = new StockBM(stockDto, donationResult.GetValue<DonationBM>(), depotResult.GetValue<DepotBM>(), itemTypeResult.GetValue<ItemTypeBM>());
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", stockBm);
            }
            catch (Exception exception) {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar el stock " + stockId + ".", exception);
            }
        }

        public ResultBM SaveStock(StockBM stockBm)
        {
            try
            {
                StockDAL stockDal = new StockDAL();
                StockDTO stockDto = null;
                ResultBM validationResult = IsValid(stockBm);

                if (validationResult.IsValid()) return validationResult;
                stockDto = new StockDTO(stockBm.Name, stockBm.Quantity, stockBm.itemType.id, stockBm.donation.id, stockBm.depot.id, stockBm.DueDate);

                stockDal.SaveStock(stockDto);
                stockBm.id = stockDto.id;

                return new ResultBM(ResultBM.Type.OK, "Se ha creado el stock.", stockBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al crear la donación.", exception);
            }
        }

        public ResultBM GetStocks()
        {
            try
            {
                StockDAL stockDal = new StockDAL();
                List<StockDTO> stocksDto = stockDal.GetStocks();
                List<StockBM> stocksBm = ConvertIntoBusinessModel(stocksDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", stocksBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar las donaciones.", exception);
            }
        }

        private List<StockBM> ConvertIntoBusinessModel(List<StockDTO> stocks)
        {
            List<StockBM> result = new List<StockBM>();
            foreach (StockDTO stock in stocks) result.Add(new StockBM(stock));
            return result;
        }

        private ResultBM IsValid(StockBM stockBm)
        {
            //if (donationBm.Items < 1)
            //    return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "La cantidad de bultos debe ser de al menos una unidad.");

            //if (donationBm.donationStatus == null)
            //    return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe selecionar un estado válido para el lote.");

            //if (donationBm.donorId == 0)
            //    return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe asignarse donador.");

            return new ResultBM(ResultBM.Type.OK);
        }

        public ResultBM GetCollection()
        {
            return GetStocks();
        }

        public ResultBM Delete(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
