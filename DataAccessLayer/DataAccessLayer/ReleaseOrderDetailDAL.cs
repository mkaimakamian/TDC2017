using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class ReleaseOrderDetailDAL
    {
        public List<ReleaseOrderDetailDTO> GetReleaseOrderDetail(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<ReleaseOrderDetailDTO> result = new List<ReleaseOrderDetailDTO>();

            sql = "SELECT * FROM release_order_detail WHERE releaseOrderId = " + id;
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

        public bool SaveReleaseOrderDetail(List<ReleaseOrderDetailDTO> releaseOrdersDetail)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO release_order_detail (releaseOrderId, stockId, quantity) VALUES ";
            foreach (ReleaseOrderDetailDTO detail in releaseOrdersDetail)
            {

                sql += "(" + detail.releaseOrderId + ", " + detail.stockId + ", " + detail.quantity + "), ";
            }
            //se remueve la coma del final
            dbsql.ExecuteNonQuery(sql.Remove(sql.Length - 2));
            return true;
        }

        public bool DeleteReleaseOrderDetail(int releaseOrderId)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "DELETE FROM release_order_detail WHERE releaseOrderId = " + releaseOrderId;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        private ReleaseOrderDetailDTO Resolve(List<String> item)
        {
            ReleaseOrderDetailDTO result = new ReleaseOrderDetailDTO();

            result.id = int.Parse(item[0]);
            result.releaseOrderId = int.Parse(item[1]);
            result.stockId = int.Parse(item[2]);
            result.quantity = int.Parse(item[3]);
            return result;
        }

    }
}
