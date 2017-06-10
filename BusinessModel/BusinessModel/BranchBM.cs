using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class BranchBM
    {
        public int id;
        public string name;
        public string comment;
        public AddressBM address;

        public BranchBM(BranchDTO branchDto, AddressBM addressBm)
        {
            this.id = branchDto.id;
            this.name = branchDto.name;
            this.comment = branchDto.comment;
            this.address = addressBm;
        }
        //public BranchBM(string name, string comment, AddressBM addressBm) {
        //}
    }
}
