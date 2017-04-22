using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class TranslationDAL
    {

        public List<TranslationDTO> GetTranslations(int languageId)
        {

            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<TranslationDTO> result = new List<TranslationDTO>();

            sql = "SELECT * ";
            sql += "FROM translation WHERE languageId = '" + languageId + "'";
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0) {
                for (int i = 0; i < reader.Count; ++i){
                    result.Add(Resolve(reader[i]));
                }
            }

            return result;
        }

        private TranslationDTO Resolve(List<String> item)
        {
            TranslationDTO result = new TranslationDTO();

            result.languageId = int.Parse(item[0]);
            result.labelCode = item[1];
            result.translation = item[2];
            return result;
        }
    }
}
