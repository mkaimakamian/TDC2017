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
        private int id;
        private string name;
        private string password;
        private bool active;
        private int languageId; //debería ser el objeto?
        private string permissionId; //debería ser objeto?
        private string hdv; //dígito verificador

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
            return this.Hdv;
        }


        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }

        public int LanguageId
        {
            get { return this.languageId; }
            set { this.languageId = value; }
        }

        public string PermissionId
        {
            get { return this.permissionId; }
            set { this.permissionId = value; }
        }

        public string Hdv
        {
            get { return this.hdv; }
            set { this.hdv = value; }
        }
    }
}
