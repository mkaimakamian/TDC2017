using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class DonorDAL
    {
        public DonorDTO GetDonor(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM donor WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        private DonorDTO Resolve(List<String> item)
        {
            DonorDTO result = new DonorDTO();
            result.donorId = int.Parse(item[0]);
            result.organizationId = int.Parse(item[1]);
            result.canBeContacted = bool.Parse(item[2]);
            return result;
        }
    }
}
