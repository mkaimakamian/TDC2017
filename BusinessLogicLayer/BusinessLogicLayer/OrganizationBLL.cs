using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTransferObject;
using BusinessModel;

namespace BusinessLogicLayer
{
    public class OrganizationBLL
    {

        public ResultBM GetOrganization(int organizationId)
        {
            try
            {
                OrganizationDAL organizationDal = new OrganizationDAL();
                OrganizationDTO organizationDto = organizationDal.GetOrganization(organizationId);
                OrganizationBM organizationBm = null;

                if (organizationDto != null)
                    organizationBm = new OrganizationBM(organizationDto);

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", organizationBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar la organización " + organizationId + ".", exception);
            }
        }
    }
}
