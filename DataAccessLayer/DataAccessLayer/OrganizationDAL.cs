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
