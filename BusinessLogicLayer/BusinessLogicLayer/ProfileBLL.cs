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
    public class ProfileBLL : BLEntity
    {
        private LogBLL log = new LogBLL();

        /// <summary>
        /// Recupera un perfil en base a su id.
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public ResultBM GetProfile(string profileId)
        {
            try {
                List<PermissionDTO> permissions;
                ProfileDAL profileDal = new ProfileDAL();
                ProfileBM result;

                string fatherCode;
                string code;
                string description;
                bool excluded;

                bool moreItemsToAdd = true;
                int maxIterations = 0; //Condición de corte del while en caso de una eventualidad
                log.AddLogInfo("Recuperando perfil", "Recuperando perfil " + profileId + ".", this);

                permissions = profileDal.GetProfile(profileId);

                if (permissions == null)
                {
                    throw new Exception("No se han encontrado permisos para el usuario.");
                }
                else if (permissions.Count == 1)
                {
                    //En caso de obtener sólo un permiso, debe crearse como hijo.
                    fatherCode = permissions[0].fatherCode;
                    code = permissions[0].code;
                    description = permissions[0].description;
                    excluded = permissions[0].excluded;
                    result = new PermissionMDL(fatherCode, code, description, excluded);
                }
                else
                {
                    //La estrategia consiste en obtener el primer elemento y considerarlo root (garantizado por la base de dato).
                    //Luego, recorrer la lista de los permisos y asignarlos al root hasta que la lista de permisos esté vacía.
                    //Para vaciar la lista, considerar la siguiente estrategia: si se agregó, se borra de la lista.
                    fatherCode = permissions[0].fatherCode;
                    code = permissions[0].code;
                    description = permissions[0].description;
                    excluded = permissions[0].excluded;
                    result = new PermissionsMDL(fatherCode, code, description, excluded);

                    while (moreItemsToAdd)
                    {
                        for (int i = permissions.Count - 1; i > 0; --i)
                        {
                            fatherCode = permissions[i].fatherCode;
                            code = permissions[i].code;
                            description = permissions[i].description;
                            excluded = permissions[i].excluded;

                            if (result.AddPermissionSorted(new PermissionMDL(fatherCode, code, description, excluded)))
                            {
                                permissions.RemoveAt(i);
                            }
                        }

                        maxIterations += 1;
                        moreItemsToAdd = (permissions.Count > 0 && maxIterations < 100);
                    }
                }

                log.AddLogInfo("Recuperando perfil", "Perfil " + profileId + " recuperado.", this);
                return new ResultBM(ResultBM.Type.OK, "Permiso recuperado.", result);
            }
            catch (Exception exception)
            {
                log.AddLogCritical("Recuperando perfil", exception.Message, this);
                return new ResultBM(ResultBM.Type.EXCEPTION, exception.Message, exception);
            }
            
        }

        /// <summary>
        /// Recupera los permisos root de los distintos menúes; no dejan de ser perfiles, pero están marcados como pertenecientes al sistema.
        /// </summary>
        /// <returns></returns>
        public ResultBM GetSystemPermissions()
        {
            try
            {
                ProfileDAL profileDal = new ProfileDAL();
                List<PermissionDTO> permissionsDto = profileDal.GetSystemPermissions();
                List<PermissionMDL> permissionBms = new List<PermissionMDL>();

                log.AddLogInfo("Recuperando perfil", "Recuperando los permisos del sistema.", this);
                foreach (PermissionDTO permission in permissionsDto)
                {
                    permissionBms.Add(new PermissionMDL(permission.fatherCode, permission.code, permission.description, permission.excluded));
                }

                log.AddLogInfo("Recuperando perfil", "Permisos del sistema recuperados.", this);
                return new ResultBM(ResultBM.Type.OK, "Lista de perfiles recuperada exitosamente", permissionBms);
            }
            catch (Exception exception)
            {
                log.AddLogCritical("Recuperando perfil", exception.Message, this);
                return new ResultBM(ResultBM.Type.EXCEPTION, exception.Message, exception);
            }

        }

        /// <summary>
        /// Recupera los perfiles creados por el usuario.
        /// </summary>
        /// <returns></returns>
        public ResultBM GetProfiles()
        {
            try {
                ProfileDAL profileDal = new ProfileDAL();
                List<PermissionDTO> permissionsDto = profileDal.GetProfiles();
                List<PermissionMDL> permissionBms = new List<PermissionMDL>();

                log.AddLogInfo("Recuperando perfil", "Recuperando perfiles creados por usuarios.", this);
                foreach (PermissionDTO permission in permissionsDto)
                {
                    permissionBms.Add(new PermissionMDL(permission.fatherCode, permission.code, permission.description, permission.excluded));
                }

                log.AddLogInfo("Recuperando perfil", "Recuperando perfiles creados por usuarios recuperados.", this);
                return new ResultBM(ResultBM.Type.OK, "Lista de perfiles recuperada exitosamente", permissionBms);
            }
            catch (Exception exception)
            {
                log.AddLogCritical("Recuperando perfil", exception.Message, this);
                return new ResultBM(ResultBM.Type.EXCEPTION, exception.Message, exception);
            }
            
        }

        private ResultBM IsValid(ProfileBM profile)
        {
            if (profile.Description.Length == 0)
            {
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "El perfil debe contar con un nombre.");
            }

            if (profile.GetChildren().Count == 0)
            {
                return new ResultBM(ResultBM.Type.EMPTY_PROFILE, "No se han asignado permisos.");
            }

            return new ResultBM(ResultBM.Type.OK);
        }

        /// <summary>
        /// Crea un perfil en base al modelo recibido, considerando únicamente los hijos del primer nivel.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public ResultBM CreateProfile(ProfileBM profile)
        {
            try
            {
                log.AddLogInfo("Creando perfil", "Creando perfil.", this);
                ProfileDAL profileDal = new ProfileDAL();
                ResultBM isValidResult = IsValid(profile);

                if (isValidResult.IsValid())
                {
                    //Se agrega el root
                    PermissionDTO root = new PermissionDTO(profile.fatherCode, profile.code, profile.Description);
                    profileDal.SaveProfile(root);
                    CreateRelation(profile);

                    log.AddLogInfo("Creando perfil", "El perfil se ha creado exitosamente.", this);
                    return new ResultBM(ResultBM.Type.OK, "Perfil creado: " + profile.Description);
                }
                else
                {
                    return isValidResult;
                }
                
            }
            catch (Exception exception)
            {
                log.AddLogCritical("Recuperando perfil", exception.Message, this);
                return new ResultBM(ResultBM.Type.EXCEPTION, exception.Message, exception);
            }
        }

        public ResultBM UpdateProfile(ProfileBM profile)
        {

            try
            {
                log.AddLogInfo("Actualizando perfil", "Creando perfil.", this);
                ProfileDAL profileDal = new ProfileDAL();
                ResultBM isValidResult = IsValid(profile);

                if (isValidResult.IsValid())
                {
                    profileDal.DeleteRelation(profile.code);
                    CreateRelation(profile);

                    log.AddLogInfo("Actualizando perfil", "El perfil se ha actualizado exitosamente.", this);
                    return new ResultBM(ResultBM.Type.OK, "Perfil actualizado.");
                }
                else
                {
                    return isValidResult;
                }
                
            }
            catch (Exception exception)
            {
                log.AddLogCritical("Recuperando perfil", exception.Message, this);
                return new ResultBM(ResultBM.Type.EXCEPTION, exception.Message, exception);
            }
            
        }

        /// <summary>
        /// Inserta las relaciones en la base de datos
        /// </summary>
        /// <param name="relations"></param>
        private void CreateRelation(ProfileBM root)
        {
            List<PermissionDTO> permissions = new List<PermissionDTO>();
            ProfileDAL profileDal = new ProfileDAL();            
            List<ProfileBM> toAnalyse = null;
            List<PermissionDTO> toExclude = new List<PermissionDTO>();

            List<ProfileBM> relations = root.GetChildren();
            log.AddLogDebug("Creando relaciones", "Creando relaciones para el perfil " + root.Description +".", this);

            //Cada elemento de primer nivel, representan los roots de las distintas jerarquías de los permisos
            //La relación con los hijos es por exclusion, es decir, se mostrarán todos a menos que se encuentren en la tabla de exclusiones
            foreach (ProfileBM permission in relations)
            {
                //Creo la relación con el padre inmediato
                permissions.Add(new PermissionDTO(permission.fatherCode, permission.code, permission.Description));
            }

            log.AddLogDebug("Creando relaciones", "Agregando relaciones Perfil - Permisos.", this);
            profileDal.SaveProfileRelation(permissions);

            foreach (ProfileBM permission in relations)
            {
                toAnalyse = permission.GetAlldescendants();
                //Agrego al padre en la lista que debe ser analizada para la exclusión
                toAnalyse.Add(permission);

                foreach (ProfileBM item in toAnalyse)
                {
                    if (item.excluded)
                        toExclude.Add(new PermissionDTO(item.fatherCode, item.code, item.Description));
                }
                if (toExclude.Count > 0)
                {
                    log.AddLogDebug("Creando relaciones", "Agregando relaciones Perfil - Exclusiones.", this);
                    profileDal.SaveProfileExclusionRelation(root.Code, toExclude);
                }
            }            
        }

        private ResultBM DeleteProfile(string code)
        {
            try
            {
                ProfileDAL profileDal = new ProfileDAL();
                
                log.AddLogInfo("Eliminando perfil", "Eliminando perfil "+ code + ".", this);

                if (profileDal.CanDeleteProfile(code))
                {
                    profileDal.DeleteProfile(code);
                    log.AddLogInfo("Eliminando perfil", "Perfil elimnado exitosamente.", this);
                    return new ResultBM(ResultBM.Type.OK, "Perfil eliminado");
                }
                else
                {
                    log.AddLogWarn("Borrando perfil", "No se puede eliminar el permiso " + code + " porque está asignado a al menos un usuario.", this);
                    return new ResultBM(ResultBM.Type.FAIL, "No se puede eliminar el permiso porque está asignado a al menos un usuario.");
                }
            }
            catch (Exception exception)
            {
                log.AddLogCritical("Recuperando perfil", exception.Message, this);
                return new ResultBM(ResultBM.Type.EXCEPTION, exception.Message, exception);
            }
        }

        public ResultBM GetCollection(Dictionary<string, string> filter = null)
        {
            return this.GetProfiles();
        }

        public ResultBM Delete(object entity)
        {
            return this.DeleteProfile(((ProfileBM) entity).Code);
        }
    }
}
