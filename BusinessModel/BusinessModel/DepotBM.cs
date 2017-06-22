using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class DepotBM
    {
        public int id;
        public string name;
        public AddressBM address;

        public DepotBM(DepotDTO depotDto, AddressBM addressBm=null)
        {
            this.id = depotDto.id;
            this.name = depotDto.name;
            this.address = addressBm;
        }
    }
}
