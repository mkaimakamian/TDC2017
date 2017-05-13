using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class DigitVerificatorDTO
    {
        public string entity;
        public string vdv;

        public DigitVerificatorDTO()
        {
        }

        public DigitVerificatorDTO(string entity, string vdv)
        {
            this.entity = entity;
            this.vdv = vdv;
        }
    }
}
