﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTransferObject;
using BusinessModel;

namespace BusinessLogicLayer
{
    public class DonorBLL
    {

        public ResultBM GetDonor(int donorId)
        {
            try
            {
                PersonBLL personBll = new PersonBLL();
                ResultBM resultPerson = null;
                OrganizationBLL organizationBll = new OrganizationBLL();
                OrganizationBM organizationBm = null;
                ResultBM resultOrganization = null;
                DonorDAL donorDal = new DonorDAL();
                DonorBM donorBm = null;                
                DonorDTO donorDto = donorDal.GetDonor(donorId);

                // Si el donador existe, deb existir la persona
                if (donorDto != null)
                {
                    resultPerson = personBll.GetPerson(donorDto.id);

                    if (resultPerson.IsValid())
                    {
                        if (resultPerson.GetValue() != null)
                        {
                            //Ver sies null O_o! que puede dar cero
                            // Podría no pertenecer a una organización, de modo tal que si no posee relación, está bien
                            if (donorDto.organizationId != null)
                            {
                                resultOrganization = organizationBll.GetOrganization(donorDto.organizationId);

                                if (resultOrganization.IsValid())
                                {
                                    if (resultOrganization.GetValue() != null)
                                    {
                                        organizationBm = resultOrganization.GetValue<OrganizationBM>();
                                    }
                                    else
                                        throw new Exception("La persona " + donorDto.id + " para el donador " + donorId + " no existe.");

                                }
                                else
                                    return resultOrganization;
                            }


                            donorBm = new DonorBM(donorDto, resultPerson.GetValue<PersonBM>(), organizationBm);
                        }
                        else
                            throw new Exception("La persona " + donorDto.id + " para el donador " + donorId + " no existe.");
                    }
                    else
                        return resultPerson;
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", donorBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar el donador " + donorId + ".", exception);
            }
        }
    }
}