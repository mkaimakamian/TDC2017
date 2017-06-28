using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel;

namespace BusinessLogicLayer
{
    //Interface para poder ser utilizada por el formulario - grilla genérico.
    public interface BLEntity
    {
        ResultBM GetCollection(Dictionary<string, string> filter = null);
        ResultBM Delete(object entity);
    }
}
