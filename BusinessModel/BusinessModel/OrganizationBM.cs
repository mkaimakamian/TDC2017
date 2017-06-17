using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class OrganizationBM
    {
        public int id;
        public string name;
        public string category;
        public string comment;
        public string phone;
        public string email;

        public OrganizationBM() { }

        public OrganizationBM(OrganizationDTO organizationDto)
        {
            this.id = organizationDto.id;
            this.name = organizationDto.name;
            this.category = organizationDto.category;
            this.comment = organizationDto.comment;
            this.phone = organizationDto.phone;
            this.email = organizationDto.email;
        }
    }
}
