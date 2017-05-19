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
        public string hdv;
        
        public UserDTO() 
        { 
        }

        //public UserDTO(int id, string name, string password, bool active, int languageId, string permissionid)
        //{
        //    this.id = id;
        //    this.name = name;
        //    this.password = password;
        //    this.active = active;
        //    this.languageId = languageId;
        //    this.permissionId = permissionid;
        //}

        //usada para el update
        public UserDTO(int id, string name, bool active, int languageId, string permissionid, string password, string hdv)
        {
            this.id = id;
            this.name = name;
            this.active = active;
            this.languageId = languageId;
            this.permissionId = permissionid;
            this.password = password;
            this.hdv = hdv;
        }

        // Usada por el crear usuario
        public UserDTO(string name, bool active, int languageId, string permissionid, string password, string hdv)
        {
            this.name = name;
            this.active = active;
            this.languageId = languageId;
            this.permissionId = permissionid;
            this.password = password;
            this.hdv = hdv;
        }
    }
}
