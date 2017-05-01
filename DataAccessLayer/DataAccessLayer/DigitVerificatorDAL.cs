using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class DigitVerificatorDAL
    {
        public List<DigitVerificatorDTO> GetEntityDigits()
        {

            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<DigitVerificatorDTO> result = new List<DigitVerificatorDTO>();

            sql = "SELECT * FROM vdv";
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

        private DigitVerificatorDTO Resolve(List<String> item)
        {
            DigitVerificatorDTO result = new DigitVerificatorDTO();

            result.entity = item[0];
            result.vdv = item[1];
            return result;
        }

    }
}
