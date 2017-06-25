using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class ItemTypeDTO
    {
        public int id;
        public string name;
        public string category;
        public string comment;
        public bool perishable;

        public ItemTypeDTO() { }

        public ItemTypeDTO(int id, string name, string category, string comment, bool perishable)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.comment = comment;
            this.perishable = perishable;
        }
    }
}
