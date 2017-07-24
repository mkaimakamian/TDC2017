using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTransferObject;
using BusinessModel;
using Helper;

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
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }


        public ResultBM UpdateOrganization(OrganizationBM organizationBm)
        {
            try
            {
                OrganizationDAL organizationDal = new OrganizationDAL();                
                OrganizationDTO organizationDto;
                ResultBM resultValidation = IsValid(organizationBm);
                
                if (!resultValidation.IsValid()) return resultValidation;

                organizationDto = new OrganizationDTO(organizationBm.name, organizationBm.category, organizationBm.comment, organizationBm.phone, organizationBm.email);
                organizationDal.SaveOrganization(organizationDto);
                organizationBm.id = organizationDto.id;

                return new ResultBM(ResultBM.Type.OK, "Organización guardada.", organizationBm);
                
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("UPDATING_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM SaveOrganization(OrganizationBM organizationBm)
        {
            try
            {
                OrganizationDAL organizationDal = new OrganizationDAL();
                OrganizationDTO organizationDto;
                ResultBM resultValidation = IsValid(organizationBm);

                if (!resultValidation.IsValid()) return resultValidation;

                organizationDto = new OrganizationDTO(organizationBm.name, organizationBm.category, organizationBm.comment, organizationBm.phone, organizationBm.email);
                organizationDal.UpdateOrganization(organizationDto);

                return new ResultBM(ResultBM.Type.OK, "Organización actualizada.", organizationBm);

            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al crear la organización " + organizationBm.name + ".", exception);
            }
        }

        private ResultBM IsValid(OrganizationBM organization)
        {
            if (organization == null)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("INVALID_VALUE_ERROR") + " (ALL)");

            if (organization.name == null || organization.name.Length == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, SessionHelper.GetTranslation("EMPTY_FIELD_ERROR") + " (NAME)");

            return new ResultBM(ResultBM.Type.OK);
        }
    }
}
