using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class StockDAL
    {
        public StockDTO GetStock(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM stock WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public List<StockDTO> GetStocks()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<StockDTO> result = new List<StockDTO>();

            sql = "SELECT * FROM stock order by donationId desc";
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

        public bool SaveStock(StockDTO stockDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO stock (name, quantity, itemTypeId, donationId, depotId, dueDate, location) VALUES (";
            sql += "'" + stockDto.name + "', ";
            sql += stockDto.quantity + ", ";
            sql += stockDto.itemTypeId + ", ";
            sql += stockDto.donationId + ", ";
            sql += stockDto.depotId + ", ";
            sql += "CONVERT(datetime, '" + stockDto.dueDate + "', 103), ";
            sql += "'" + stockDto.loaction + "'";
            sql += ");SELECT @@IDENTITY";
            stockDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool UpdateStock(StockDTO stockDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "UPDATE stock SET ";
            sql += "name = '" + stockDto.name + "',  ";
            sql += "quantity = " + stockDto.quantity + ", ";
            sql += "itemTypeId = " + stockDto.itemTypeId + ",  ";
            sql += "donationId = " + stockDto.donationId + ",  ";
            sql += "depotId = " + stockDto.depotId + ", ";
            sql += "dueDate = CONVERT(datetime, '" + stockDto.dueDate + "', 103), ";
            sql += "location = '" + stockDto.loaction  + "' ";
            sql += "WHERE id = " + stockDto.id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        private StockDTO Resolve(List<String> item)
        {
            StockDTO result = new StockDTO();

            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.quantity = int.Parse(item[2]);
            result.itemTypeId = int.Parse(item[3]);
            result.donationId = int.Parse(item[4]);
            result.depotId = int.Parse(item[5]);
            result.dueDate = DateTime.Parse(item[6]);
            result.loaction = item[7];
            return result;

        }
    }
}
