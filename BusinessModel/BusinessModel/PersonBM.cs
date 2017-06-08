using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class PersonBM
    {
        public int id;
        public string name;
        public string lastName;
        public DateTime birthdate;
        public string email;
        public string phone;
        public char gender;
        public int dni;
        public AddressBM address;

        public PersonBM() { }

        public PersonBM(PersonDTO personDto, AddressBM address)
        {
            this.id = personDto.id;
            this.name = personDto.name;
            this.lastName = personDto.lastName;
            this.birthdate = personDto.birthdate;
            this.email = personDto.email;
            this.phone = personDto.phone;
            this.gender = personDto.gender;
            this.dni = personDto.dni;
            this.address = address;
        }

        public PersonBM(string name, string lastName, DateTime birthDate, string email, string phone, char gender, int dni, AddressBM address)
        {
            this.name = name;
            this.lastName = lastName;
            this.birthdate = birthDate;
            this.email = email;
            this.phone = phone;
            this.gender = gender;
            this.dni = dni;
            this.address = address;
        }
    }
}
