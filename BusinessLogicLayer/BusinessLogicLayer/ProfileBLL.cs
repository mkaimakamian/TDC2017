using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAccessLayer;
using BusinessModel;

namespace BusinessLogicLayer
{
    public class ProfileBLL
    {
        public ProfileMDL GetProfile(string profileId)
        {
            List<PermissionDTO> permissions;
            ProfileDAL profileDal = new ProfileDAL();
            ProfileMDL result;

            string fatherCode;
            string code;
            string description;

            bool moreItemsToAdd = true;
            int maxIterations = 0; //Condición de corte del while en caso de una eventualidad

            permissions = profileDal.GetProfile(profileId);

            if (permissions == null) {
                 throw new Exception("No se han encontrado permisos para el usuario.");
            }
            else if (permissions.Count == 1)
            {
                //En caso de obtener sólo un permiso, debe crearse como hijo.
                fatherCode = permissions[0].fatherCode;
                code = permissions[0].code;
                description = permissions[0].description;
                result = new PermissionMDL(fatherCode, code, description);
            }
            else
            {
                //La estrategia consiste en obtener el primer elemento y considerarlo root (garantizado por la base de dato).
                //Luego, recorrer la lista de los permisos y asignarlos al root hasta que la lista de permisos esté vacía.
                //Para vaciar la lista, considerar la siguiente estrategia: si se agregó, se borra de la lista.
                fatherCode = permissions[0].fatherCode;
                code = permissions[0].code;
                description = permissions[0].description;
                result = new PermissionsMDL(fatherCode, code, description);

                while (moreItemsToAdd)
                {
                    for (int i = permissions.Count-1; i > 0; --i)
                    {
                        fatherCode = permissions[i].fatherCode;
                        code = permissions[i].code;
                        description = permissions[i].description;

                        if (result.AddPermission(new PermissionMDL(fatherCode, code, description)))
                        {
                            permissions.RemoveAt(i);
                        }
                    }

                    maxIterations += 1;
                    moreItemsToAdd = (permissions.Count > 0 && maxIterations < 100);
                }
            }
            
            return result;
        }
    }
}
