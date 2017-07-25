using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAccessLayer;
using BusinessModel;
using Helper;

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
                            return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR"));
                    }
                    else
                        return addressResult;
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", branchBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RETRIEVING_ERROR") + " " + exception.Message, exception);
            }
        }
    }
}
