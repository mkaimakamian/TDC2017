using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class ReleaseOrderDAL
    {

        public ReleaseOrderDTO GetReleaseOrder(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM release_order WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public bool SaveReleaseOrder(ReleaseOrderDTO releaseOrderDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO release_order (beneficiaryId, comment, released, received, status) VALUES (";
            sql += releaseOrderDto.beneficiaryId + ", ";
            sql += "'" + releaseOrderDto.comment + "', ";
            sql += "null,";
            sql += "null,";
            sql += "'" + releaseOrderDto.status + "' ";
            sql += ");SELECT @@IDENTITY";
            releaseOrderDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public List<ReleaseOrderDTO> GetReleaseOrders()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<ReleaseOrderDTO> result = new List<ReleaseOrderDTO>();

            sql = "SELECT * FROM release_order";
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

        public bool UpdateReleaseOrder(ReleaseOrderDTO releaseOrderDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "UPDATE address SET ";
            sql += "beneficiaryId = " + releaseOrderDto.beneficiaryId + ", ";
            sql += "comment = '" + releaseOrderDto.comment + "', ";
            sql += "status = '" + releaseOrderDto.status + "' ";
            sql += "WHERE id = " + releaseOrderDto.id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        private ReleaseOrderDTO Resolve(List<String> item)
        {
            ReleaseOrderDTO result = new ReleaseOrderDTO();

            result.id = int.Parse(item[0]);
            result.beneficiaryId = int.Parse(item[1]);
            result.comment = item[2];
            if (item[3] != null && item[3].Length > 0) result.released = DateTime.Parse(item[3]);
            if (item[4] != null && item[4].Length > 0) result.received = DateTime.Parse(item[4]);
            result.status = item[5];
            return result;
        }
    }
}
