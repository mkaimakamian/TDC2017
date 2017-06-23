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

            sql = "SELECT d.*, SUM(CASE WHEN s.quantity IS NULL THEN 0 ELSE s.quantity END) stocked FROM donation d LEFT JOIN stock s ON s.donationId = d.id WHERE d.id = " + id;
            sql += " GROUP BY d.id, d.items, d.arrival, d.statusId, d.donorId, d.comment, d.volunteerId";
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

            sql = "SELECT d.*, SUM(CASE WHEN s.quantity IS NULL THEN 0 ELSE s.quantity END) stocked FROM donation d LEFT JOIN stock s ON s.donationId = d.id GROUP BY d.id, d.items, d.arrival, d.statusId, d.donorId, d.comment, d.volunteerId";
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

        //Sólo recupera aquellos con estado 1 (Recibido)
        public List<DonationDTO> GetAvaliableDonations()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<DonationDTO> result = new List<DonationDTO>();

            sql = "SELECT d.*, SUM(CASE WHEN s.quantity IS NULL THEN 0 ELSE s.quantity END) stocked ";
            sql += "FROM donation d LEFT JOIN stock s ON s.donationId = d.id ";
            sql += "WHERE d.statusId = 1 ";            
            sql += "GROUP BY d.id, d.items, d.arrival, d.statusId, d.donorId, d.comment, d.volunteerId ";
            //sql += "HAVING SUM(CASE WHEN s.quantity IS NULL THEN 0 ELSE s.quantity END) < items";

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
            sql += "volunteerId = " + (donationDto.volunteerId == 0 ? "null" :  donationDto.volunteerId.ToString()) + " ";
            sql += "WHERE id = " + donationDto.id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool UpdateStatusToStored(int id, int statusId)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "UPDATE donation set statusId = " + statusId + " WHERE id IN ( ";
            sql += "SELECT d.id FROM donation d LEFT JOIN stock s ON s.donationId = d.id ";
            sql += "WHERE d.id = "+ id + " ";
            sql += "GROUP BY d.id, d.items, d.arrival, d.statusId, d.donorId, d.comment, d.volunteerId ";
            sql += "HAVING SUM(CASE WHEN s.quantity IS NULL THEN 0 ELSE s.quantity END) = items)";
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

            //No forma parte del modelo de datos per sé
            result.stocked = int.Parse(item[7]);
            return result;
        }
    }
}
