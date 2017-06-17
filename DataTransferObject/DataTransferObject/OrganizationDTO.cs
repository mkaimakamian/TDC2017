using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class OrganizationDTO
    {
        public int id;
        public string name;
        public string category;
        public string comment;
        public string phone;
        public string email;

        public OrganizationDTO() { }

        public OrganizationDTO(string name, string category, string comment, string phone, string email)
        {
            this.name = name;
            this.category = category;
            this.comment = comment;
            this.phone = phone;
            this.email = email;
        }
    }
}
