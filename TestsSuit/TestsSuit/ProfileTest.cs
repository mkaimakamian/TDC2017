using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;
using BusinessModel;

namespace TestsSuit
{
    [TestClass]
    public class ProfileTest
    {
        //casos a cubrir
        //1. Agregar jerarquía completa                 cubierto por CreatePermission
        //2. Agregar jerarquía y un elemento suelto     cubierto por CreatePermission
        //3. Agregar jerarquía y eliminar un hijo
        //4. Agregar jerarquía y otra que se encuentre incluida
        //5. Agregar jerarquía y otra que tenga de los que incluya al menos un elemento
        //6.Agregar jerarquia y otra que no sean conexas
        //7. Agregar un hijo y que arme la jerarquía hasta arriba


        [TestMethod]
        public void CreatePermission()
        {
            //Crea un permiso compuesto por una jerarquía y una opcion sin jerarquia

            // Recupero un permiso con jerarquías anidadas
            ProfileBLL profileBll = new ProfileBLL();
            ResultBM result = profileBll.GetProfile("GE001");
            ProfileBM withChildren = result.GetValue<PermissionsMDL>();

            //Se recupera un permiso que no pertenece a la jerarquía anterior
            result = profileBll.GetProfile("OP001");
            ProfileBM noChildren = result.GetValue<PermissionMDL>();

            PermissionsMDL newProfile = new PermissionsMDL(null, "XX999", "TestProfile");
            newProfile.AddPermission(withChildren);
            newProfile.AddPermission(noChildren);

            profileBll.CreateProfile(newProfile);
        }

        [TestMethod]
        public void CreatePatentPermission()
        {
            //Crea un permiso patente, esperando que me cree toda la jerarquía necesaria para acceder

            //ProfileBLL profileBll = new ProfileBLL();
            //ResultBM result = profileBll.GetProfile("OP001");
            //ProfileBM noChildren = result.GetValue<PermissionMDL>();

            //PermissionsMDL newProfile = new PermissionsMDL(null, "XX999", "TestProfile");
            //newProfile.AddPermission(noChildren);

            //profileBll.CreateProfile(newProfile);
        }

        //[TestMethod]
        //public void CreateInclusivePermission()
        //{
        //    //Crea un permiso compuesto usando una jerarquía que incluye a otra, esperando que la jerarquía incluyente
        //    //sea la que sobreviva

        //    // Recupero un permiso con jerarquías anidadas
        //    ProfileBLL profileBll = new ProfileBLL();
        //    ResultBM result = profileBll.GetProfile("GE999");
        //    ProfileBM container = result.GetValue<PermissionsMDL>();

        //    //Se recupera un permiso que no pertenece a la jerarquía anterior
        //    result = profileBll.GetProfile("GE001");
        //    ProfileBM contained = result.GetValue<PermissionsMDL>();

        //    PermissionsMDL newProfile = new PermissionsMDL(null, "XX999", "TestProfile");
        //    newProfile.AddPermission(container);
        //    newProfile.AddPermission(contained);

        //    newProfile.GetHierarchyAsList();
        //    profileBll.CreateProfile(newProfile);
        //}
    }
}
