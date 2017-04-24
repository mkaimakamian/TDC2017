using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class UserDTO
    {
        public int id;
        public string name;
        public string password;
        public bool active;
        public int languageId;
        public string permissionId;

        public UserDTO() 
        { 
        }

        public UserDTO(int id, string name, string password, bool active, int languageId, string permissionid)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.active = active;
            this.languageId = languageId;
            this.permissionId = permissionid;
        }

        public UserDTO(int id, string name, bool active, int languageId, string permissionid)
        {
            this.id = id;
            this.name = name;
            this.active = active;
            this.languageId = languageId;
            this.permissionId = permissionid;
        }
    }
}
