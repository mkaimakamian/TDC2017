using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class UserBM: DigitVeryficator
    {
        public int id;
        public string name;
        public string password;
        public bool active;
        public int languageId; //debería ser el objeto?
        public string permissionId; //debería ser objeto?
        public string hdv; //dígito verificador

        public UserBM(UserDTO userDto)
        {
            this.id = userDto.id;
            this.name = userDto.name;
            this.active = userDto.active;
            this.languageId = userDto.languageId;
            this.permissionId = userDto.permissionId;
            this.password = userDto.password;
            this.hdv = userDto.hdv;
        }

        public UserBM(string name, bool active, int languageId, string permissionId, string password)
        {
            //this.id = id;
            this.name = name;
            this.active = active;
            this.languageId = languageId;
            this.permissionId = permissionId;
            this.password = password;
            //this.hdv = hdv;
        }

        public string GetSeed()
        {
            return this.name + this.password + this.active + this.languageId + this.permissionId + this.password;
        }

        public string GetDigit()
        {
            return this.hdv;
        }
    }
}
