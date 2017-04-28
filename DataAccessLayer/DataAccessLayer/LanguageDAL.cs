using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class LanguageDAL
    {

        public LanguageDTO GetLanguage(int languageId)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<LanguageDTO> result = new List<LanguageDTO>();

            sql = "SELECT * FROM language where id = " + languageId;

            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }


        /// <summary>
        /// Recupera el listado de idiomas disponibles en el sistema.
        /// </summary>
        /// <returns></returns>
        public List<LanguageDTO> GetLanguages()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<LanguageDTO> result = new List<LanguageDTO>();

            sql = "SELECT * FROM language ORDER BY name";

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

        private LanguageDTO Resolve(List<String> item)
        {
            LanguageDTO result = new LanguageDTO();
            result.id = int.Parse(item[0]);
            result.name = item[1];
            return result;
        }
    }
}
