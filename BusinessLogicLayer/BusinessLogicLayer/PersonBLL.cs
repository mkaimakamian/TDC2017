﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTransferObject;
using BusinessModel;
using Helper;

namespace BusinessLogicLayer
{
    public class PersonBLL
    {

        public ResultBM GetPerson(int personId)
        {
            try
            {
                AddressBLL addressBll = new AddressBLL();
                ResultBM resultAddress = null;
                PersonDAL personDal = new PersonDAL();
                PersonBM personBm = null;                
                PersonDTO personDto = personDal.GetPerson(personId);

                // Si la persona existe, debería existir la dirección.
                if (personDto != null)
                {
                    resultAddress = addressBll.GetAddress(personDto.addressId);
                    if (! resultAddress.IsValid()) return resultAddress;

                    if (resultAddress.GetValue() != null)
                        personBm = new PersonBM(personDto, resultAddress.GetValue<AddressBM>());
                    else
                        throw new Exception(SessionHelper.GetTranslation("RETRIEVING_ERROR") + " addressId " + personDto.addressId);
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", personBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }

        /// <summary>
        /// Crea una persona.
        /// </summary>
        /// <param name="personBm"></param>
        /// <returns></returns>
        public ResultBM SavePerson(PersonBM personBm)
        {
            try
            {
                AddressBLL addressBll = new AddressBLL();
                ResultBM addressResult;
                PersonDAL personDal = new PersonDAL();
                PersonDTO personDto;
                ResultBM validationResult;
                
                validationResult = IsValid(personBm);
                if (!validationResult.IsValid()) return validationResult;
                
                addressResult = addressBll.SaveAddress(personBm.address);
                if (!addressResult.IsValid()) return addressResult;

                personDto = new PersonDTO(personBm.id, personBm.name, personBm.lastName, personBm.Birthdate, personBm.Email, personBm.phone, personBm.gender, personBm.dni, personBm.address.id);
                personDal.SavePerson(personDto);
                personBm.id = personDto.id;

                return new ResultBM(ResultBM.Type.OK, "Se ha creado la persona con el nombre " + personBm.name + " " + personBm.lastName+ ".", personBm);
               
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("SAVING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM UpdatePerson(PersonBM personBm)
        {
            try {
                AddressBLL addressBll = new AddressBLL();
                ResultBM addressResult;
                PersonDAL personDal = new PersonDAL();
                PersonDTO personDto;
                ResultBM validationResult;

                validationResult = IsValid(personBm);
                if (!validationResult.IsValid()) return validationResult;

                addressResult = addressBll.UpdateAddress(personBm.address);
                if (!addressResult.IsValid()) return addressResult;

                personDto = new PersonDTO(personBm.id, personBm.name, personBm.lastName, personBm.Birthdate, personBm.Email, personBm.phone, personBm.gender, personBm.dni, personBm.address.id);
                personDal.UpdatePerson(personDto);

                return new ResultBM(ResultBM.Type.OK, "Se ha actualizado la persona con el nombre " + personBm.name + " " + personBm.lastName + ".", personBm);

            }
            catch (Exception exception) {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("UPDATING_ERROR") + " " + exception.Message, exception);
            }
        }


        private ResultBM IsValid(PersonBM personBm)
        {
            if (personBm.name == null || personBm.name.Length == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (NAME)");

            if (personBm.lastName == null || personBm.lastName.Length == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (LASTNAME)");

            if (personBm.dni < 1 || personBm.dni.ToString().Length < 8)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("INVALID_VALUE_ERROR") + " (DNI)");
            
            return new ResultBM(ResultBM.Type.OK);
        }
    }
}
