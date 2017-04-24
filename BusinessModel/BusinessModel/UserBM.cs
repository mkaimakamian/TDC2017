using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class UserBM
    {
        public int id;
        public string name;
        public string password;
        public bool active;
        public int languageId; //debería ser el objeto?
        public string permissionId; //debería ser objeto?

        public UserBM(UserDTO userDto)
        {
            this.id = userDto.id;
            this.name = userDto.name;
            this.active = userDto.active;
            this.languageId = userDto.languageId;
            this.permissionId = userDto.permissionId;
            //Adrede no se instancia con el password
        }
    }
}
