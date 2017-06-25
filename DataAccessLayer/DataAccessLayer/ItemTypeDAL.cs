using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class ItemTypeDAL
    {

        public ItemTypeDTO GetItemType(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM item_type WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public bool SaveItemType(ItemTypeDTO itemTypeDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO item_type (name, category, comment, perishable) VALUES (";
            sql += "'" + itemTypeDto.name + "', ";
            sql += "'" + itemTypeDto.category + "', ";
            sql += "'" + itemTypeDto.comment + "', ";
            sql += "'" + itemTypeDto.perishable + "'";
            sql += ");SELECT @@IDENTITY";
            itemTypeDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool UpdateItemType(ItemTypeDTO itemTypeDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "UPDATE item_type SET ";
            sql += "name = '" + itemTypeDto.name + "',  ";
            sql += "category = '" + itemTypeDto.category + "', ";
            sql += "comment = '" + itemTypeDto.comment + "',  ";
            sql += "perishable = '" + itemTypeDto.perishable + "' ";
            sql += "WHERE id = " + itemTypeDto.id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }


        public List<ItemTypeDTO> GetItemTypes()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<ItemTypeDTO> result = new List<ItemTypeDTO>();

            sql = "SELECT * FROM item_type";
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

        private ItemTypeDTO Resolve(List<String> item)
        {
            ItemTypeDTO result = new ItemTypeDTO();

            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.category = item[2];
            result.comment = item[3];
            result.perishable = bool.Parse(item[4]);
            return result;
        }

    }
}
