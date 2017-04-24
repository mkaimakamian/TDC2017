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
    public class TranslationBLL
    {
        /// <summary>
        /// Devuelve una lista de objetos del tipo TranslationMDL.
        /// Si no encuentra traducciones, la lista se devuelvé vacía.
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public List<TranslationBM> GetTranslations(int languageId)
        {
            List<TranslationDTO> translations;
            TranslationDAL translationDal = new TranslationDAL();
            List<TranslationBM> result = new List<TranslationBM>();

            translations = translationDal.GetTranslations(languageId);

            foreach (TranslationDTO translation in translations)
            {
                //NO SE DEBE PASAR DTO
                result.Add(new TranslationBM(translation));
            }

            return result;
        }

        /// <summary>
        /// Devuelve un dicionario con las taducciones.
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        //public Dictionary<String, String> GetPlainTranslations(int languageId)
        //{
        //    List<TranslationDTO> translations;
        //    TranslationDAL translationDal = new TranslationDAL();
        //    Dictionary<String, String> result = new Dictionary<String, String>();

        //    translations = translationDal.GetTranslations(languageId);

        //    foreach (TranslationDTO translation in translations)
        //    {
        //        result.Add(translation.labelCode, translation.translation);
        //    }

        //    return result;
        //}
    }
}
