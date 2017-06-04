using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class AddressDAL
    {

        public AddressDTO GetAddress(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM address WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public List<AddressDTO> GetAddresses()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<AddressDTO> result = new List<AddressDTO>();

            sql = "SELECT * FROM address";
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

        private AddressDTO Resolve(List<String> item)
        {
            AddressDTO result = new AddressDTO();

            result.id = int.Parse(item[0]);
            result.street = item[1];
            result.number = int.Parse(item[2]);
            result.apartment = item[3];
            result.neighborhood = item[4];
            result.comment = item[5];
            result.countryIso = item[6];
            return result;
        }
    }
}
