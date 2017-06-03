using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class CountryDAL
    {
        public CountryDTO GetCountry(string iso2)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM country WHERE iso2 = " + iso2;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public List<CountryDTO> GetCountries()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<CountryDTO> result = new List<CountryDTO>();

            sql = "SELECT * FROM country";
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

        private CountryDTO Resolve(List<String> item)
        {
            CountryDTO result = new CountryDTO();

            result.iso2 = item[0];
            result.iso3 = item[1];
            result.name = item[2];
            return result;
        }
    }
}
