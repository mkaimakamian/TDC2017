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
            //sql += "SELECT v.permissionIdBranch fatherCode, v.permissionIdLeaf code, pe.description, pe.system ";
            //sql += "FROM (SELECT  p.* FROM permisos p UNION SELECT NULL,  '" + profileId + "') v ";
            //sql += "LEFT JOIN permission pe ON  pe.id = v.permissionIdLeaf ORDER BY 1, 2";

            sql += "SELECT v.permissionIdBranch fatherCode, v.permissionIdLeaf code, pe.description, pe.system, CASE WHEN px.excluded IS NULL THEN 'false' ELSE 'true' END excluded ";
            sql += "FROM (SELECT  p.* FROM permisos p UNION SELECT NULL,  '" + profileId + "') v ";
            sql += "LEFT JOIN permission pe ON  pe.id = v.permissionIdLeaf ";
            sql += "LEFT JOIN permission_exclusion px ON px.id = '" + profileId  + "' AND (px.excluded = v.permissionIdLeaf OR px.excluded = v.permissionIdBranch) ";
            sql += "group by v.permissionIdBranch, v.permissionIdLeaf, pe.description, pe.system,  CASE WHEN  px.excluded  IS NULL  THEN 'false' ELSE 'true'  END ";
            sql += "ORDER BY 1, 2";


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

        public List<PermissionDTO> GetSystemPermissions()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<PermissionDTO> result = new List<PermissionDTO>();

            // Recupera los roots de todos los permisos de sistema
            sql = "SELECT DISTINCT null  fatherCode, p.id, p.description, p.system, 'false' excluded FROM permission p INNER JOIN permission_hierarchy ph ON ph.permissionIdBranch = p.id AND p.system = 1 ";
            sql += "WHERE p.id NOT IN ";
            sql += "(SELECT DISTINCT ph.permissionIdLeaf FROM permission p INNER JOIN permission_hierarchy ph ON ph.permissionIdBranch = p.id AND p.system = 1)";

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

        /// <summary>
        /// Devuelve un listado con todos los permisos (no jerarquía)
        /// </summary>
        /// <returns></returns>
        public List<PermissionDTO> GetProfiles()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<PermissionDTO> result = new List<PermissionDTO>();

            sql = "SELECT null  fatherCode, id, description, system, 'false' excluded FROM permission";

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

        public bool SaveProfile(PermissionDTO permissionDto) {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO permission (id, description, system) VALUES (";
            sql += "'" + permissionDto.code + "', ";
            sql += "'" + permissionDto.description + "', ";
            sql += "'" + permissionDto.system + "'";
            sql += ")";
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool SaveProfileRelation(List<PermissionDTO> permissionsDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO permission_hierarchy (permissionIdBranch, permissionIdLeaf) VALUES ";
            foreach (PermissionDTO permission in permissionsDto) {

                sql += "('" + permission.fatherCode + "', '" + permission.code+"'), ";
            }
            //se remueve la coma del final
            dbsql.ExecuteNonQuery(sql.Remove(sql.Length -2));
            return true;
        }

        public bool SaveProfileExclusionRelation(string fatherCode, List<PermissionDTO> permissionsDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO permission_exclusion (id, excluded) VALUES ";
            foreach (PermissionDTO permission in permissionsDto) {

                sql += "('" + fatherCode + "', '" + permission.code + "'), ";
            }
            //se remueve la coma del final
            dbsql.ExecuteNonQuery(sql.Remove(sql.Length -2));
            return true;
        }

        /// <summary>
        /// Elimina el perfil según el código provisto por parámetro.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool DeleteProfile(string code) {
            DBSql dbsql = new DBSql();
            String sql;
            sql = "DELETE FROM permission_exclusion WHERE id = '" + code + "'";
            dbsql.ExecuteNonQuery(sql);

            sql = "DELETE FROM permission_hierarchy WHERE permissionIdBranch = '" + code + "'";
            dbsql.ExecuteNonQuery(sql);

            sql = "DELETE FROM permission WHERE id = '" + code + "'";
            dbsql.ExecuteNonQuery(sql);

            return true;
        }

        /// <summary>
        /// Devuelve true si no se está utilizando el perfil y puede ser eliminado.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CanDeleteProfile(string code)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT 1 FROM users WHERE permissionId = '" + code +"'";
            reader = dbsql.executeReader(sql);

            return reader.Count == 0;
        }

        private PermissionDTO Resolve(List<String> item)
        {
            PermissionDTO result = new PermissionDTO();
            result.fatherCode = item[0];
            result.code = item[1];
            result.description = item[2];
            result.system = bool.Parse(item[3]);
            result.excluded = bool.Parse(item[4]);
            return result;
        }
    }
}
