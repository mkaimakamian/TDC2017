using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class DonationDAL
    {

        public DonationDTO GetDonation(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM donation WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public bool SaveDonor(DonationDTO donationDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO donation (items, arrival, lot, statusId, donorId, comment, volunteerId) VALUES (";
            sql += donationDto.items + ", ";
            sql += "GETDATE(), ";
            sql += "'" + donationDto.lot + "', ";
            sql += donationDto.statusId + ", ";
            sql += donationDto.donorId + ", ";
            sql += "'" + donationDto.comment + "', ";
            sql += "'" + donationDto.volunteerId + "'";
            sql += ");SELECT @@IDENTITY";
            donationDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }

        private DonationDTO Resolve(List<String> item)
        {
            DonationDTO result = new DonationDTO();
            result.id = int.Parse(item[0]);
            result.items = int.Parse(item[1]);
            result.arrival = DateTime.Parse(item[2]);
            result.lot = item[3];
            result.statusId = int.Parse(item[4]);
            result.donorId = int.Parse(item[5]);
            result.comment = item[6];            
            result.volunteerId = int.Parse(item[7]);            
            return result;
        }
    }
}
