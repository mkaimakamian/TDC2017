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

            sql = "SELECT p.*, d.id donorId, d.organizationId, d.CanBeContacted FROM donor d INNER JOIN person p ON p.id = d.personId WHERE d.id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public List<DonorDTO> GetDonors()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<DonorDTO> result = new List<DonorDTO>();

            sql = "SELECT p.*, d.id donorId, d.organizationId, d.CanBeContacted FROM donor d INNER JOIN person p ON p.id = d.personId WHERE d.deleted = 0";
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

        public bool SaveDonor(DonorDTO donorDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO donor (personId, organizationId, canBeContacted) VALUES (";
            sql += donorDto.id + ", ";
            sql += donorDto.organizationId == 0 ? "null, " : donorDto.organizationId  + ", ";
            sql += "'" + donorDto.canBeContacted + "'";
            sql += ");SELECT @@IDENTITY";
            donorDto.donorId = dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool UpdateDonor(DonorDTO donorDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "UPDATE donor SET ";
            sql += "personId = " + donorDto.id + ", ";
            sql += "organizationId = " + (donorDto.organizationId == 0 ? "null" : donorDto.organizationId.ToString()) + ", ";
            sql += "canBeContacted = '" + donorDto.canBeContacted + "' ";
            sql += "WHERE id = " + donorDto.donorId;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool DeleteDonor(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "UPDATE donor SET deleted = 1 WHERE id = " + id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        private DonorDTO Resolve(List<String> item)
        {
            DonorDTO result = new DonorDTO();
            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.lastName = item[2];
            result.birthdate = DateTime.Parse(item[3]);
            result.email = item[4];
            result.phone = item[5];
            result.gender = Char.Parse(item[6]);
            result.dni = int.Parse(item[7]);
            result.addressId = int.Parse(item[8]);
            result.donorId = int.Parse(item[9]);
            result.organizationId = int.Parse(item[10] == "" ? "0" : item[10]);
            result.canBeContacted = bool.Parse(item[11]);
            return result;
        }
    }
}
