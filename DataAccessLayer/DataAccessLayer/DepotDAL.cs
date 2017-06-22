using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class DepotDAL
    {

        public DepotDTO GetDepot(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM depot WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public List<DepotDTO> GetDepots()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<DepotDTO> result = new List<DepotDTO>();

            sql = "SELECT * FROM depot";
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

        private DepotDTO Resolve(List<String> item)
        {
            DepotDTO result = new DepotDTO();

            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.addressId = int.Parse(item[2]);
            return result;
        }
    }

}
