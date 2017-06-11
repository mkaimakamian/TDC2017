using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class BranchDAL
    {

        public BranchDTO GetBranch(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM branch WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        private BranchDTO Resolve(List<String> item)
        {
            BranchDTO result = new BranchDTO();

            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.comment = item[2];
            result.addressId = int.Parse(item[3]);
            return result;
        }
    }
}
