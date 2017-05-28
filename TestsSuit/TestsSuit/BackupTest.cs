using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;
using BusinessModel;

namespace TestsSuit
{
    [TestClass]
    public class BackupTest
    {
        public string path = "";
        [TestInitialize]
        public void Inicializar()
        {
            string fecha = DateTime.Now.ToString("yyyymmddHHmm");
            this.path = @"c:\tmp\rhoddion_" + fecha + ".bkp";
        }

        [TestMethod]
        public void CreateBackup()
        {
            BackupBLL bkp = new BackupBLL();
            //ResultBM result = bkp.PerformBackup(path);
            //Assert.IsTrue(result.IsValid(), "El backup debería haberse creado.");
        }

        [TestMethod]
        public void RestoreBackup()
        {
            BackupBLL bkp = new BackupBLL();
            //ResultBM result = bkp.PerformRestore(path);
            //Assert.IsTrue(result.IsValid(), "El backup debería haberse restaurado.");
        }
                    

    }
}
