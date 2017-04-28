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
    public class LanguageBLL
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

        public List<LanguageBM> GetLanguages()
        {
            LanguageDAL languageDal = new LanguageDAL();
            List<LanguageBM> result = new List<LanguageBM>();
            List<LanguageDTO> languages  = languageDal.GetLanguages();

            foreach (LanguageDTO language in languages)
            {
                result.Add(new LanguageBM(language.id, language.name));
            }

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
            LanguageBM result = new LanguageBM(language.id, language.name, translations);

            return result;
        }

        //public bool SaveLanguage(LanguageBM languageBm)
        //{            
        //    LanguageDTO languageDto = new LanguageDTO(languageBm.Id, languageBm.Name);
        //    LanguageDAL languageDal = new LanguageDAL();
        //    return languageDal.SaveLanguage(languageDto);
        //}
        
    }
}
