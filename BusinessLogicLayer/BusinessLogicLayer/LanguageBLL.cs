using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class LanguageBLL
    {
        private LanguageDTO languageDto;
        private List<TranslationBLL> translationBll;

        /// <summary>
        /// Inicializa esta instancia con el idioma.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LanguageBLL GetLanguage(int id) {
            LanguageDAL languageDal = new LanguageDAL();
            languageDto = languageDal.GetLanguage(id);
            return this;

            //DEBEERÍA DEVOLVER UN BUSINESSMODEL!
        }
    }
}
