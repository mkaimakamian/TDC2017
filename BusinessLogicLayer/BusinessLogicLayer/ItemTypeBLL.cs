﻿using System;
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
    public class ItemTypeBLL : BLEntity
    {

        public ResultBM GetItemType(int itemTypeId)
        {
            try
            {
                ItemTypeDAL itemTypeDal = new ItemTypeDAL();
                ItemTypeBM itemTypeBm = null;
                ItemTypeDTO itemTypeDto = itemTypeDal.GetItemType(itemTypeId);

                if (itemTypeDto != null)
                    itemTypeBm = new ItemTypeBM(itemTypeDto);

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", itemTypeBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM GetItemTypes()
        {
            try
            {
                ItemTypeDAL itemTypeDal = new ItemTypeDAL();
                List<ItemTypeDTO> itemTypeDtos = itemTypeDal.GetItemTypes();
                List<ItemTypeBM> itemTypesBm = ConvertIntoBusinessModel(itemTypeDtos);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", itemTypesBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM SaveItemType(ItemTypeBM itemTypeBm)
        {
            try
            {
                ItemTypeDAL itemTypeDal = new ItemTypeDAL();
                ItemTypeDTO itemTypeDto = null;

                ResultBM validationResult = IsValid(itemTypeBm);
                if (!validationResult.IsValid()) return validationResult;

                itemTypeDto = new ItemTypeDTO(itemTypeBm.id, itemTypeBm.Name, itemTypeBm.category, itemTypeBm.Comment, itemTypeBm.Perishable);
                itemTypeDal.SaveItemType(itemTypeDto);
                itemTypeBm.id = itemTypeDto.id;

                return new ResultBM(ResultBM.Type.OK, "Se ha creado el ítem " + itemTypeBm.Name + ".", itemTypeBm);

            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("SAVING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM UpdateItemType(ItemTypeBM itemTypeBm)
        {
            try
            {
                ItemTypeDAL itemTypeDal = new ItemTypeDAL();
                ItemTypeDTO itemTypeDto = null;

                ResultBM validationResult = IsValid(itemTypeBm);
                if (!validationResult.IsValid()) return validationResult;

                itemTypeDto = new ItemTypeDTO(itemTypeBm.id, itemTypeBm.Name, itemTypeBm.category, itemTypeBm.Comment, itemTypeBm.Perishable);
                itemTypeDal.UpdateItemType(itemTypeDto);

                return new ResultBM(ResultBM.Type.OK, "Se ha creado el ítem " + itemTypeBm.Name + ".", itemTypeBm);

            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("UPDATING_ERROR") + " " + exception.Message, exception);
            }
        }

        private ResultBM IsValid(ItemTypeBM itemTypeBm)
        {
            if (itemTypeBm.Name == null || itemTypeBm.Name.Length == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (NAME)");

            if (itemTypeBm.category == null || itemTypeBm.category.Length == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (CATEGORY)");
            
            return new ResultBM(ResultBM.Type.OK);
        }

        private List<ItemTypeBM> ConvertIntoBusinessModel(List<ItemTypeDTO> itemTypes)
        {
            List<ItemTypeBM> result = new List<ItemTypeBM>();
            foreach (ItemTypeDTO itemType in itemTypes)
            {
                result.Add(new ItemTypeBM(itemType));
            }
            return result;
        }

        public ResultBM GetCollection(Dictionary<string, string> filter = null)
        {
            return this.GetItemTypes();
        }

        public ResultBM Delete(object entity)
        {
            try
            {
                ItemTypeDAL itemTypeDal = new ItemTypeDAL();
                ItemTypeBM itemTypeBm = entity as ItemTypeBM;

                if (!itemTypeDal.IsInUse(itemTypeBm.id))
                {
                    itemTypeDal.DeleteItemType(itemTypeBm.id);
                    return new ResultBM(ResultBM.Type.OK, "Se ha eliminado el registro.", itemTypeBm);
                } else
                {
                    return new ResultBM(ResultBM.Type.FAIL, SessionHelper.GetTranslation("ARTICLE_TYPE_UNDELETEABLE_ERROR"), itemTypeBm);
                }

            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("DELETING_ERROR") + " " + exception.Message, exception);
            }
        }
    }
}
