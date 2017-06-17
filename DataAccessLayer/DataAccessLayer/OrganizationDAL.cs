using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class OrganizationDAL
    {

        public OrganizationDTO GetOrganization(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM organization WHERE id= " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public bool SaveOrganization(OrganizationDTO organizationDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO organization (name, category, comment, phone, email) VALUES (";
            sql += "'" + organizationDto.name + "', ";
            sql += "'" + organizationDto.category + "', ";
            sql += "'" + organizationDto.comment + "', ";
            sql += "'" + organizationDto.phone + "', ";
            sql += "'" + organizationDto.email + "'";
            sql += ");SELECT @@IDENTITY";
            organizationDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }


        private OrganizationDTO Resolve(List<String> item)
        {
            OrganizationDTO result = new OrganizationDTO();

            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.category = item[2];
            result.comment = item[3];
            result.phone = item[4];
            result.email = item[5];
            return result;
        }
    }
}
