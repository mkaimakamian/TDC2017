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

        public List<DonationDTO> GetDonations()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<DonationDTO> result = new List<DonationDTO>();

            sql = "SELECT * FROM donation";
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                for (int i = 0; i < reader.Count; ++i)
                {
                    result.Add(Resolve(reader[i]));
                }
            }

            return result;
        }

        public bool SaveDonation(DonationDTO donationDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO donation (items, arrival, statusId, donorId, comment, volunteerId) VALUES (";
            sql += donationDto.items + ", ";
            sql += "GETDATE(), ";
            sql += donationDto.statusId + ", ";
            sql += donationDto.donorId + ", ";
            sql += "'" + donationDto.comment + "', ";
            sql += "null";
            sql += ");SELECT @@IDENTITY";
            donationDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool UpdateDonation(DonationDTO donationDto)
        {
            DBSql dbsql = new DBSql();
            String sql;
            
            sql = "UPDATE donation SET ";
            sql += "items = " + donationDto.items + ",  ";
            sql += "statusId = " + donationDto.statusId + ", ";
            sql += "donorId = " + donationDto.donorId + ",  ";
            sql += "comment = '" + donationDto.comment + "',  ";
            sql += "volunteerId = " + donationDto.volunteerId + " ";
            sql += "WHERE id = " + donationDto.id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }
        private DonationDTO Resolve(List<String> item)
        {
            DonationDTO result = new DonationDTO();
            result.id = int.Parse(item[0]);
            result.items = int.Parse(item[1]);
            result.arrival = DateTime.Parse(item[2]);
            result.statusId = int.Parse(item[3]);
            result.donorId = int.Parse(item[4]);
            result.comment = item[5];
            result.volunteerId = int.Parse(item[6].Length == 0 ? "0" : item[6]);
            return result;
        }
    }
}
