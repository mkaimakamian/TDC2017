using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class TranslationMDL
    {
        public int languageId; //debería ser el objeto
        public string labelCode; //debería ser el objeto
        public string translation;

        public TranslationMDL(TranslationDTO translationDto)
        {
            this.languageId = translationDto.languageId;
            this.labelCode = translationDto.labelCode;
            this.translation = translationDto.translation;
        }

    }
}
