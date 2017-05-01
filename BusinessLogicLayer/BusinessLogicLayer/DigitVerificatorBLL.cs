using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel;
using Helper;
using DataAccessLayer;
using DataTransferObject;

namespace BusinessLogicLayer
{
    public class DigitVerificatorBLL
    {
        const string USER_TABLE = "users";

        /// <summary>
        /// Devuelve true si la consistencia de los datos propios de la entidad, se mantienen (horizontal)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsHorizontallyConsistent(DigitVeryficator entity)
        {
            return  SecurityHelper.IsEquivalent(entity.GetSeed(), entity.GetDigit());
        }

        /// <summary>
        /// Controla la consistencia vertical de todas las entidades críticas
        /// </summary>
        /// <returns></returns>
        public bool IsVerticallyConsistent()
        {
            Dictionary<string, string> entityToCheck = new Dictionary<string, string>();
            DigitVerificatorDAL dvDal = new DigitVerificatorDAL();
            List<DigitVerificatorDTO> digits;

            UserBLL userBll = new UserBLL();
            List<DigitVeryficator> users = userBll.GetUsers().Cast<DigitVeryficator>().ToList();

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
                    //TODO -  error tipado
                    return false;
                }
            }

            return true;
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
                toCheck += entity.GetDigit();
            }

            return toCheck;
        }

    }
}
