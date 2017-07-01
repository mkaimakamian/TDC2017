using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class BeneficiaryBM: PersonBM
    {
        public int beneficiaryId;
        public int destination;
        public int ages;
        public int health;
        public int accessibility;
        public int majorProblem;

        public BeneficiaryBM() { }

        public BeneficiaryBM(BeneficiaryDTO beneficiaryDto, AddressBM address = null) {
            this.id = beneficiaryDto.id;
            this.beneficiaryId = beneficiaryDto.beneficiaryId;
            this.destination = beneficiaryDto.destination;
            this.ages = beneficiaryDto.ages;
            this.health = beneficiaryDto.health;
            this.accessibility = beneficiaryDto.accessibility;
            this.majorProblem = beneficiaryDto.majorProblem;
            this.address = address;
        }
    }
}
