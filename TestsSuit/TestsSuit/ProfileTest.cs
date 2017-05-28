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
        //1. Crear un perfil con una sóla jerarquía completas
        //2. Crear perfil con dos jerarquías completas
        //3. Crear perfil con una jerarquía incompleta
        //4. Crear perfil con una jerarquía completa y otra incompleta
        //5. Crear un perfil sin nombre
        //6. Crear perfil con nombre pero sin permisos
        //7. Crear perfil y borrarlo


        [TestMethod]
        public void CreatePermission()
        {
            //// Recupero un permiso con jerarquías anidadas
            //ProfileBLL profileBll = new ProfileBLL();
            //ResultBM result = profileBll.GetProfile("GE001");
            //ProfileBM withChildren = result.GetValue<PermissionsMDL>();

            ////Se recupera un permiso que no pertenece a la jerarquía anterior
            //result = profileBll.GetProfile("OP001");
            //ProfileBM noChildren = result.GetValue<PermissionMDL>();

            //PermissionsMDL newProfile = new PermissionsMDL(null, "XX999", "TestProfile", false);
            //newProfile.AddPermission(withChildren);
            //newProfile.AddPermission(noChildren);

            //profileBll.CreateProfile(newProfile);
        }

       
    }
}
