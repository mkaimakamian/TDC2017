using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class ProfileDAL
    {

        public List<PermissionDTO> GetProfile(String profileId)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<PermissionDTO> result = new List<PermissionDTO>();

            // Se recupera el árbol de jerarquías utilizando CTE
            sql = "WITH permisos (permissionIdBranch, permissionIdLeaf) AS ( ";
            sql += "SELECT pr.permissionIdBranch, pr.permissionIdLeaf ";
            sql += "FROM permission_hierarchy pr WHERE pr.permissionIdBranch = '" + profileId + "' ";
            sql += "UNION ALL ";
            sql += "SELECT pr.permissionIdBranch, pr.permissionIdLeaf ";
            sql += "FROM permission_hierarchy pr INNER JOIN permisos p ";
            sql += "ON p.permissionIdLeaf = pr.permissionIdBranch) ";

            // Al final queda: id del permiso, descripción, permiso por el que se accedió, padre // autoreferencia
            // El permiso por el que se busca siempre se excluye, razón por la que hay que incluirlo con un union
            sql += "SELECT v.permissionIdBranch fatherCode, v.permissionIdLeaf code, pe.description ";
            sql += "FROM (SELECT  p.* FROM permisos p UNION SELECT NULL,  '" + profileId + "') v ";
            sql += "LEFT JOIN permission pe ON  pe.id = v.permissionIdLeaf ORDER BY 1, 2";

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

        private PermissionDTO Resolve(List<String> item)
        {
            PermissionDTO result = new PermissionDTO();
            result.fatherCode = item[0];
            result.code = item[1];
            result.description = item[2];
            return result;
        }
    }
}
