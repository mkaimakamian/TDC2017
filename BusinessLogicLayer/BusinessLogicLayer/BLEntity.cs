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
        ResultBM GetCollection();
        ResultBM Delete(object entity);
    }
}
