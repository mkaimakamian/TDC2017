using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class LanguageBM
    {
        private int id;
        private string name;

        public LanguageBM(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set {this.name = value;}
        }
    }
}
