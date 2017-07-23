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
                StockBM stockBm = null;
                StockDTO stockDto = stockDal.GetStock(stockId);

                //Si existe el stock, las relaciones deberían existir... TODAS
                if (stockDto != null)
                {
                    donationResult = donationBll.GetDonation(stockDto.donationId);
                    if (!donationResult.IsValid()) return donationResult;
                    if (donationResult.GetValue() == null) throw new Exception(SessionHelper.GetTranslation("RETRIEVING_ERROR") + " donationId " + stockDto.donationId);

                    depotResult = depotBll.GetDepot(stockDto.depotId);
                    if (!depotResult.IsValid()) return depotResult;
                    if (depotResult.GetValue() == null) throw new Exception(SessionHelper.GetTranslation("RETRIEVING_ERROR") + " depotId " + stockDto.depotId);

                    itemTypeResult = itemTypeBll.GetItemType(stockDto.itemTypeId);
                    if (!itemTypeResult.IsValid()) return itemTypeResult;
                    if (itemTypeResult.GetValue() == null) throw new Exception(SessionHelper.GetTranslation("RETRIEVING_ERROR") + " itemTypeId " + stockDto.itemTypeId);

                    stockBm = new StockBM(stockDto, donationResult.GetValue<DonationBM>(), depotResult.GetValue<DepotBM>(), itemTypeResult.GetValue<ItemTypeBM>());
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", stockBm);
            }
            catch (Exception exception) {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM SaveStock(StockBM stockBm)
        {
            try
            {
                StockDAL stockDal = new StockDAL();
                StockDTO stockDto = null;
                ResultBM validationResult = IsValid(stockBm);

                if (!validationResult.IsValid()) return validationResult;
                stockDto = new StockDTO(stockBm.id, stockBm.Name, stockBm.Quantity, stockBm.itemType.id, stockBm.donation.id, stockBm.depot.id, stockBm.DueDate, stockBm.Location);

                stockDal.SaveStock(stockDto);
                stockBm.id = stockDto.id;

                new DonationBLL().UpdateToStoredStatusIfApply(stockBm.donation.id);

                return new ResultBM(ResultBM.Type.OK, "Se ha creado el stock.", stockBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("SAVING_ERROR") + " " + exception.Message, exception);
            }
        }
        
        public ResultBM UpdateStock(StockBM stockBm)
        {
            try
            {
                StockDAL stockDal = new StockDAL();
                StockDTO stockDto = null;
                ResultBM validationResult = IsValid(stockBm);

                if (!validationResult.IsValid()) return validationResult;
                stockDto = new StockDTO(stockBm.id, stockBm.Name, stockBm.Quantity, stockBm.itemType.id, stockBm.donation.id, stockBm.depot.id, stockBm.DueDate, stockBm.Location);

                stockDal.UpdateStock(stockDto);

                new DonationBLL().UpdateToStoredStatusIfApply(stockBm.donation.id);

                return new ResultBM(ResultBM.Type.OK, "Se ha creado el stock.", stockBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("UPDATING_ERROR") + " " + exception.Message, exception);
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
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        // TODO - modificar para que solo traiga lo asignable
        public ResultBM GetAvailableStocks()
        {
            try
            {
                StockDAL stockDal = new StockDAL();
                List<StockDTO> stocksDto = stockDal.GetAvailableStock();
                List<StockBM> stocksBm = ConvertIntoBusinessModel(stocksDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", stocksBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        private List<StockBM> ConvertIntoBusinessModel(List<StockDTO> stocks)
        {
            List<StockBM> result = new List<StockBM>();
            foreach (StockDTO stock in stocks) result.Add(new StockBM(stock, GetDonation(stock)));
            return result;
        }

        //No está bueno esto, pero me permite recuperar el voluntario. Poco performante... pero no hay tiempo.
        private DonationBM GetDonation(StockDTO stock)
        {
            ResultBM donationResult = new DonationBLL().GetDonation(stock.donationId);
            return donationResult.GetValue<DonationBM>();
        }

        private ResultBM IsValid(StockBM stockBm)
        {
            if (stockBm.Name == null || stockBm.Name.Length == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (NAME)");

            if (stockBm.Quantity == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (ITEMS < 1)");
            
            if (!(stockBm.GetAmountItemsToStockWithoutThis() <= stockBm.donation.Items * 2 && stockBm.GetAmountItemsToStockWithoutThis() > 1))
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (TOO MUCH ITEMS)");

            if (stockBm.itemType.Perishable && stockBm.DueDate == null)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (DATE)");

            if (stockBm.Location == null || stockBm.Location.Length == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (LOCATION)");


            return new ResultBM(ResultBM.Type.OK);
        }

        public ResultBM GetCollection(Dictionary<string, string> filter = null)
        {
            return GetStocks();
        }

        public ResultBM Delete(object entity)
        {
            try
            {
                StockDAL stockDal = new StockDAL();
                DonationBLL donationBll = new DonationBLL();
                StockBM stockBm = entity as StockBM;

                if (!stockDal.IsInUse(stockBm.id))
                {
                    stockDal.DeleteStock(stockBm.id);
                    donationBll.UpdateToReceivedStatus(stockBm.donation.id);
                    return new ResultBM(ResultBM.Type.OK, "Se ha eliminado el registro.", stockBm);
                }
                else
                {
                    return new ResultBM(ResultBM.Type.FAIL, SessionHelper.GetTranslation("STOCK_UNDELETEABLE_ERROR"), stockBm);
                }

            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("DELETING_ERROR") + " " + exception.Message, exception);
            }           
        }
    }
}
