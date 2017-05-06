using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;

namespace TestsSuit
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void CreateLogs()
        {
            LogBLL logBll = new LogBLL();
            logBll.AddLogInfo("CREAR", "Creando log del tipo Info.", logBll);                        
            logBll.AddLogWarn("CREAR", "Creando log del tipo Warn.", logBll);
            logBll.AddLogCritical("CREAR", "Creando log del tipo Critical.", logBll);
            logBll.AddLogDebug("CREAR", "Creando log del tipo Debug.", logBll);
        }
    }
}
