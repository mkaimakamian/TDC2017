using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAccessLayer;
using BusinessModel;

namespace BusinessLogicLayer
{
    public class BranchBLL
    {
        public ResultBM GetBranch(int branchId)
        {
            try
            {
                AddressBLL addressBll = new AddressBLL();
                ResultBM addressResult = null;
                BranchDAL branchDal = new BranchDAL();
                BranchDTO branchDto = branchDal.GetBranch(branchId);
                BranchBM branchBm = null;

                //Si la sede existe, debería existir la dirección
                if (branchDto != null)
                {
                    addressResult = addressBll.GetAddress(branchDto.addressId);

                    if (addressResult.IsValid())
                    {
                        if (addressResult.GetValue() != null)
                            branchBm = new BranchBM(branchDto, addressResult.GetValue<AddressBM>());
                        else
                            throw new Exception("La dirección con id " + branchBm.address.id + "para la sede " + branchId + " no existe.");
                    }
                    else
                        return addressResult;
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", branchBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar la sede " + branchId + ".", exception);
            }
        }
    }
}
