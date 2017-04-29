﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class LanguageDTO
    {
        public int id;
        public string name;

        public LanguageDTO() 
        { 
        }
        
        public LanguageDTO(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}