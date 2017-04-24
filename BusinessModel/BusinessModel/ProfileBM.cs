using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public abstract class ProfileBM
    {
        public string fatherCode;
        public string code;
        public string description;

        public abstract bool AddPermission(ProfileBM permission);
        public abstract void DeletePermission(string code);
        public abstract ProfileBM GetPermission(string code);
        public abstract bool HasPermission(string code);
        public abstract bool IsFather();
    }

    /// <summary>
    /// Clase "hijo" de los permisos.
    /// </summary>
    public class PermissionMDL:ProfileBM
    {
 
        public PermissionMDL(string fatherCode, string code, string description)
        {
            this.fatherCode = fatherCode;
            this.code = code;
            this.description = description;
        }

        public override bool AddPermission(ProfileBM permission)
        {
            //Responsabilidad del padre
            throw new NotImplementedException();
        }

        public override void DeletePermission(string code)
        {
            //responsabilidad del padre
            throw new NotImplementedException();
        }

        public override ProfileBM GetPermission(string code)
        {
            return this;
        }

        public override bool HasPermission(string code)
        {
            return this.code == code;
        }

        public override bool IsFather()
        {
            return false;
        }
    }

    /// <summary>
    /// Clase "padre" de los permisos.
    /// </summary>
    public class PermissionsMDL : ProfileBM
    {
        List<ProfileBM> permissions;

        public PermissionsMDL(string fatherCode, string code, string description)
        {
            this.fatherCode = fatherCode;
            this.code = code;
            this.description = description;
            this.permissions = new List<ProfileBM>();
        }

        /// <summary>
        /// Si el permiso pasado por parámetro pertenece a la jerarquía, se encarga de agregarlo convirtiendo -de ser necesario- hijos en padre.
        /// Considerar el root como punto de partida para agregar un elemento, garantiza que todo el árbol es recorrido.
        /// </summary>
        /// <param name="permission"></param>
        public override bool AddPermission(ProfileBM permission)
        {
            //La estrategia es simple: si el objeto es padre del permiso, se agrega directamente; en caso contrario, se busca recursivamente 
            //un hijo que aplique como padre. Si el hijo que aplica no es padre, se procede a la conversión del mismo.
            if (this.code == permission.fatherCode)
            {
                this.permissions.Add(permission);
                return true;
            }
            else
            {
                foreach (ProfileBM profile in permissions)
                {
                    if (profile.IsFather())
                    {
                        if (profile.AddPermission(permission))
                        {
                            return true;
                        }

                    } else if (profile.code == permission.fatherCode) {
                        //El hijo que no es padre debe convertirse en padre.
                        PermissionsMDL newFather = new PermissionsMDL(profile.fatherCode, profile.code, profile.description);
                        newFather.AddPermission(permission);
                        this.permissions.Remove(profile);
                        this.permissions.Add(newFather);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Remueve el permiso que corresponda con el código pasado por parámetro, y sus dependientes.
        /// </summary>
        /// <param name="code"></param>
        public override void DeletePermission(string code)
        {
            foreach (ProfileBM profile in permissions)
            {
                if (profile.code == code)
                {
                    permissions.Remove(profile);
                    return;
                }
            }        
        }

        public override ProfileBM GetPermission(string code)
        {
            return this;
        }

        /// <summary>
        /// Devuelve true si en la jerarquía existe el permiso pasado por parámetro.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public override bool HasPermission(string code)
        {
            if (this.code == code)
            {
                return true;
            }
            else
            {
                foreach (ProfileBM profile in permissions)
                {
                    if (profile.HasPermission(profile.code))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool IsFather()
        {
            return true;
        }
    }
}
