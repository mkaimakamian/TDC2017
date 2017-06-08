using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class PersonDTO
    {
        public int id;
        public string name;
        public string lastName;
        public DateTime birthdate;
        public string email;
        public string phone;
        public char gender;
        public int dni;
        public int addressId;

        public PersonDTO() { }
        public PersonDTO(string name, string lastName, DateTime birthdate, string email, string phone, char gender, int dni, int addressId)
        {
            this.name = name;
            this.lastName = lastName;
            this.birthdate = birthdate;
            this.email = email;
            this.phone = phone;
            this.gender = gender;
            this.dni = dni;
            this.addressId = addressId;
        }
    }
}
