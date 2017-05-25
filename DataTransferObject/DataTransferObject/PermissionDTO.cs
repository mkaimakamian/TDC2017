using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class PermissionDTO
    {
        public string fatherCode;
        public string code;
        public string description;
        public bool system;

        public PermissionDTO()
        {
        }

        public PermissionDTO(string fatherCode, string code, string description, bool system = false)
        {
            this.fatherCode = fatherCode;
            this.code = code;
            this.description = description;
            this.system = system;
        }
    }
}
