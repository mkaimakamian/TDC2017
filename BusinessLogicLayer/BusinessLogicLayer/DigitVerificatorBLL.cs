﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel;
using Helper;
using DataAccessLayer;
using DataTransferObject;
using Helper;

namespace BusinessLogicLayer
{
    public class DigitVerificatorBLL
    {
        const string USER_TABLE = "users";

        public ResultBM UpdateVerticallDigit()
        {
            try
            {
                //TODO - debería soportar varias entidades
                DigitVerificatorDAL dvDal = new DigitVerificatorDAL();
                UserBLL userBll = new UserBLL();

                ResultBM usersBms = userBll.GetUsers();
                List<DigitVeryficator> users = usersBms.GetValue<List<UserBM>>().Cast<DigitVeryficator>().ToList();

                string digit = SecurityHelper.Encrypt(GetStringToCheck(users));

                DigitVerificatorDTO digitDto = new DigitVerificatorDTO(USER_TABLE, digit);
                bool result = dvDal.UpdateEntityDigit(digitDto);

                if (result) return new ResultBM(ResultBM.Type.OK, "Dígito verificador vertical actualizado.");
                else return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("UPDATING_ERROR"));
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("UPDATING_ERROR") + " " + exception.Message, exception);
            }
        }


        /// <summary>
        /// Devuelve true si la consistencia de los datos propios de la entidad, se mantienen (horizontal)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultBM IsHorizontallyConsistent(DigitVeryficator entity)
        {
            bool result = SecurityHelper.IsEquivalent(entity.GetSeed(), entity.GetDigit());

            if (result) return new ResultBM(ResultBM.Type.OK, "Dígito horizontal correcto.");
            else return new ResultBM(ResultBM.Type.CORRUPTED_DATABASE, SessionHelper.GetTranslation("CORRUPTED_DATABASE_ERROR"));
        }

        /// <summary>
        /// Controla la consistencia vertical de todas las entidades críticas
        /// </summary>
        /// <returns></returns>
        public ResultBM IsVerticallyConsistent()
        {
            Dictionary<string, string> entityToCheck = new Dictionary<string, string>();
            DigitVerificatorDAL dvDal = new DigitVerificatorDAL();
            List<DigitVerificatorDTO> digits;

            UserBLL userBll = new UserBLL();
            ResultBM usersBms = userBll.GetUsers();
            List<DigitVeryficator> users = usersBms.GetValue<List<UserBM>>().Cast<DigitVeryficator>().ToList();

            //TODO - Evaluar la posibilidad de ir a buscar a vdv el listado de entidades, y luego ir  a buscarlas

            // La estrategia consiste en crear un diccionario que posea como key la entidad a controlar, 
            //y como value un string construido en base a los dígitos horizontales de cada uno de sus registros
            entityToCheck.Add(USER_TABLE, this.GetStringToCheck(users));

            digits = dvDal.GetEntityDigits();

            // En base a las entidades que poseen integridad vertical, se recorre la lista de entidades a chequear
            foreach (DigitVerificatorDTO entityVDV in digits)
            {
                if (!SecurityHelper.IsEquivalent(entityToCheck[entityVDV.entity], entityVDV.vdv))
                {
                    return new ResultBM(ResultBM.Type.CORRUPTED_DATABASE, SessionHelper.GetTranslation("CORRUPTED_DATABASE_ERROR") + " (VERTICAL - " + entityVDV.entity+")");
                }
            }

            return new ResultBM(ResultBM.Type.OK, "Dígito vertical correcto.");
        }

        /// <summary>
        /// Devuelve el hash MD5 para el dígito verificador.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CreateDigit(DigitVeryficator entity)
        {
            // Encapsula la estrategia para crear el dígito vrificador; en este caso es el mismo algoritmo usado para hashear el password.
            return SecurityHelper.Encrypt(entity.GetSeed());
        }

        /// <summary>
        /// Devuelve los hash concatenados en base a los dígitos horizontales de un conjunto de registros de una misma entidad.
        /// y devuelve la cadena que debe compararse contra el d
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        private string GetStringToCheck(List<DigitVeryficator> entities)
        {
            string toCheck = "";
            foreach (DigitVeryficator entity in entities)
            {
                toCheck += SecurityHelper.Encrypt(entity.GetSeed());
            }

            return toCheck;
        }

    }
}
