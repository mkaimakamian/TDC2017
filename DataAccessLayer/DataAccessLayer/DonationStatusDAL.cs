using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class DonationStatusDAL
    {
        public DonationStatusDTO GetDonationStatus(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM donation_status WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        private DonationStatusDTO Resolve(List<String> item)
        {
            DonationStatusDTO result = new DonationStatusDTO();
            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.description = item[2];
            return result;
        }
    }
}
