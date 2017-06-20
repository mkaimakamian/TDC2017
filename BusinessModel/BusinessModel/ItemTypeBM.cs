using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class ItemTypeBM
    {
        public int id;
        private string name;
        public string category;
        public string comment;
        private bool perishable;

        public enum Category
        {
            EDIBLE,
            INDUMENTARY,
            MEDICINE,
            CONSTRUCTION,
            OTHER
        };

        public ItemTypeBM() { }

        public ItemTypeBM(ItemTypeDTO itemTypeDto)
        {
            this.id = itemTypeDto.id;
            this.name = itemTypeDto.name;
            this.category = itemTypeDto.category;
            this.comment = itemTypeDto.comment;
            this.perishable = itemTypeDto.perishable;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string CategoryType
        {
            get { return this.category; }
            set { this.category = value; }
        }

        public bool Perishable
        {
            get { return this.perishable; }
            set { this.perishable = value; }
        }

        public bool IsEdible()
        {
            return IsCurrentCategory(Category.EDIBLE);
        }

        public bool IsIndumentary()
        {
            return IsCurrentCategory(Category.INDUMENTARY);
        }

        public bool IsMedicine()
        {
            return IsCurrentCategory(Category.MEDICINE);
        }

        public bool IsConstruction()
        {
            return IsCurrentCategory(Category.CONSTRUCTION);
        }

        public bool IsOther()
        {
            return IsCurrentCategory(Category.OTHER);
        }

        private bool IsCurrentCategory(Category category)
        {
            return this.category == category.ToString();
        }
    }
}
