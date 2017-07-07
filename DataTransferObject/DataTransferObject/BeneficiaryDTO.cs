using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class BeneficiaryDTO: PersonDTO
    {
        public int beneficiaryId;
        public int personId;
        public int destination;
        public int ages;
        public int health;
        public int accessibility;
        public int majorProblem;

        public BeneficiaryDTO() { }

        public BeneficiaryDTO(int personId, int beneficiaryId, int destination, int ages, int health, int accessibility, int majorProblem)
        {
            this.id = personId;
            this.beneficiaryId = beneficiaryId;
            this.destination = destination;
            this.ages = ages;
            this.health = health;
            this.accessibility = accessibility;
            this.majorProblem = majorProblem;
        }
    }
}
