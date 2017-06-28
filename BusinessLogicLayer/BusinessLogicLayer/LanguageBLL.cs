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
    public class LanguageBLL: BLEntity
    {
        //private LanguageDTO languageDto;
        //private List<TranslationBLL> translationBll;

        /// <summary>
        /// Inicializa esta instancia con el idioma.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public LanguageBLL GetLanguage(int id) {
        //    LanguageDAL languageDal = new LanguageDAL();
        //    languageDto = languageDal.GetLanguage(id);
        //    return this;

        //    //DEBEERÍA DEVOLVER UN BUSINESSMODEL!
        //}

        public ResultBM GetLanguages()
        {
            LanguageDAL languageDal = new LanguageDAL();
            List<LanguageDTO> languages  = languageDal.GetLanguages();
            List<LanguageBM> languageBms = ConvertIntoBusinessModel(languages);
            ResultBM result = new ResultBM(ResultBM.Type.OK, "Recuperación de los idiomas exitoso.", languageBms);
            return result;
        }

        /// <summary>
        /// Devuelve el Business Model del idioma, incluyendo las traducciones asociadas.
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public LanguageBM GetLanguage(int languageId)
        {
            TranslationBLL translationBll = new TranslationBLL();
            List<TranslationBM> translations = translationBll.GetTranslations(languageId);
            LanguageDAL languageDal = new LanguageDAL();            
            LanguageDTO language = languageDal.GetLanguage(languageId);
            LanguageBM result = ConvertIntoBusinessModel(language, translations);
            return result;
        }

        /// <summary>
        /// Convierte el DTO en BM.
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private LanguageBM ConvertIntoBusinessModel(LanguageDTO entity, List<TranslationBM> translation = null)
        {
            LanguageBM result = new LanguageBM();
            result.Id = entity.id;
            result.Name = entity.name;
            result.Translations = translation;
            return result;
        }

        /// <summary>
        /// Convierte un listado de objetos DTO en uno de BM.
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        private List<LanguageBM> ConvertIntoBusinessModel(List<LanguageDTO> entities)
        {
            List<LanguageBM> result = new List<LanguageBM>();
            foreach (LanguageDTO entity in entities)
            {
                result.Add(ConvertIntoBusinessModel(entity));
            }
            return result;
        }

        public ResultBM GetCollection(Dictionary<string, string> filter = null)
        {
            return this.GetLanguages();
        }


        public ResultBM Delete(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
