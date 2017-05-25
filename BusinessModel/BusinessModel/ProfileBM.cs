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
        private string description;
        public bool excluded;

        public abstract bool AddPermissionSorted(ProfileBM permission); //agrega el permiso ordenadamente
        public abstract bool AddPermission(ProfileBM permission); //agrega el permiso como hijo de este objeto
        public abstract void DeletePermission(string code); //elimina el permiso
        public abstract ProfileBM GetPermission(string code); //devuelve el permiso según el código
        public abstract bool HasPermission(string code); //evalúa si existe el permiso
        public abstract bool IsFather(); //evalúa si el objeto es del tipo padre
        public abstract List<ProfileBM> GetChildren(); //devuelve la lista de hijos inmediatos
        //public abstract List<ProfileBM> GetHierarchyAsList();
        public abstract List<ProfileBM> GetAlldescendants(); //devuelve la lista de todos los descendientes

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

    }

    /// <summary>
    /// Clase "hijo" de los permisos.
    /// </summary>
    public class PermissionMDL:ProfileBM
    {
 
        public PermissionMDL(string fatherCode, string code, string description, bool excluded)
        {
            this.fatherCode = fatherCode;
            this.code = code;
            this.Description = description;
            this.excluded = excluded;
        }

        public override bool AddPermissionSorted(ProfileBM permission)
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
            return this.code == code && !this.excluded;
        }

        public override bool IsFather()
        {
            return false;
        }

        public override List<ProfileBM> GetChildren()
        {
            throw new NotImplementedException();
        }

        public override bool AddPermission(ProfileBM permission)
        {
            throw new NotImplementedException();
        }

        //public override List<ProfileBM> GetHierarchyAsList()
        //{
        //    throw new NotImplementedException();
        //}

        public override List<ProfileBM> GetAlldescendants()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Clase "padre" de los permisos.
    /// </summary>
    public class PermissionsMDL : ProfileBM
    {
        List<ProfileBM> permissions;

        public PermissionsMDL(string fatherCode, string code, string description, bool excluded)
        {
            this.fatherCode = fatherCode;
            this.code = code;
            this.Description = description;
            this.excluded = excluded;
            this.permissions = new List<ProfileBM>();
        }

        /// <summary>
        /// Si el permiso pasado por parámetro pertenece a la jerarquía, se encarga de agregarlo convirtiendo -de ser necesario- hijos en padre.
        /// Considerar el root como punto de partida para agregar un elemento, garantiza que todo el árbol es recorrido.
        /// </summary>
        /// <param name="permission"></param>
        public override bool AddPermissionSorted(ProfileBM permission)
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
                        if (profile.AddPermissionSorted(permission))
                        {
                            return true;
                        }

                    } else if (profile.code == permission.fatherCode) {
                        //El hijo que no es padre debe convertirse en padre.
                        PermissionsMDL newFather = new PermissionsMDL(profile.fatherCode, profile.code, profile.Description, profile.excluded);
                        newFather.AddPermissionSorted(permission);
                        this.permissions.Remove(profile);
                        this.permissions.Add(newFather);
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool AddPermission(ProfileBM permission)
        {
            permission.fatherCode = this.code;
            return this.AddPermissionSorted(permission);
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
            return this;//debería buscar todos
        }

        /// <summary>
        /// Devuelve true si en la jerarquía existe el permiso pasado por parámetro.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public override bool HasPermission(string toFind)
        {
            if (this.code == toFind && !this.excluded)
            {
                return true;
            }
            else
            {
                foreach (ProfileBM profile in permissions)
                {
                    if (profile.HasPermission(toFind))
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

        public override List<ProfileBM> GetChildren()
        {
            return this.permissions;
        }


        /// <summary>
        /// Devuelve los elementos de la jerarquía en formato lista, sin duplicar
        /// </summary>
        /// <returns></returns>
        //public override List<ProfileBM> GetHierarchyAsList()
        //{
        //    List<ProfileBM> grandChildren = new List<ProfileBM>();
        //    List<ProfileBM> children = new List<ProfileBM>();
        //    List<ProfileBM> aux;


        //    foreach (ProfileBM profile in this.permissions)
        //    {
        //        aux = new List<ProfileBM>();

        //        //Si el elemento existe, no se debe agregar
        //        if (!Exist(children, profile))
        //        {
        //            children.Add(profile);
        //        }
                
        //        if (profile.IsFather()) {
        //            grandChildren = profile.GetHierarchyAsList();
        //        }

        //        //Una vez que se recuperaron los hijos de las descendencias, debe controlarse que se puedan agregar
        //        for (int i = 0; i < grandChildren.Count; ++i)
        //        {
        //            if (!Exist(children, grandChildren[i]))
        //            {
        //                aux.Add(grandChildren[i]);
        //            }
        //        }
        //        children.AddRange(aux); 
        //    }
        //    return children;
        //}

        //private bool Exist( List<ProfileBM> list, ProfileBM element)
        //{
        //    bool found = false;
        //    for (int c = 0; c < list.Count && !found; ++c)
        //    {
        //        found = list[c].code == element.code;
        //    }
        //    return found;
        //}

        public override List<ProfileBM> GetAlldescendants()
        {
            List<ProfileBM> profiles = new List<ProfileBM>();
            foreach (ProfileBM profile in GetChildren())
            {
                if (profile.IsFather())
                {
                   profiles.AddRange(profile.GetAlldescendants());
                }
                else
                {
                    profiles.Add(profile);
                }
            }
            profiles.Add(this);
            return profiles;
        }
    }
}
