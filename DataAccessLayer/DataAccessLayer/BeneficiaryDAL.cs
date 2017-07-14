using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class BeneficiaryDAL
    {
        public BeneficiaryDTO GetBeneficiary(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT p.*, b.id beneficiaryId, destination, ages, health, accessibility, majorProblem FROM beneficiary b INNER JOIN person p ON p.id = b.personId WHERE b.id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public List<BeneficiaryDTO> GetBeneficiaries()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<BeneficiaryDTO> result = new List<BeneficiaryDTO>();

            sql = "SELECT p.*, b.id beneficiaryId, destination, ages, health, accessibility, majorProblem FROM beneficiary b INNER JOIN person p ON p.id = b.personId WHERE b.deleted = 0";
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

        public bool SaveBeneficiary(BeneficiaryDTO beneficiaryDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO beneficiary (personId, destination, ages, health, accessibility, majorProblem) VALUES (";
            sql += beneficiaryDto.id + ", ";
            sql += beneficiaryDto.destination + ", ";
            sql += beneficiaryDto.ages + ", ";
            sql += beneficiaryDto.health + ", ";
            sql += beneficiaryDto.accessibility + ", ";
            sql += beneficiaryDto.majorProblem + " ";
            sql += ");SELECT @@IDENTITY";
            beneficiaryDto.beneficiaryId = dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool UpdateDonor(BeneficiaryDTO beneficiaryDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "UPDATE beneficiary SET ";
            sql += "personId = " + beneficiaryDto.id + ", ";
            sql += "destination = " + beneficiaryDto.destination + ", ";
            sql += "ages = " + beneficiaryDto.ages + ", ";
            sql += "health = " + beneficiaryDto.health + ", ";
            sql += "accessibility = " + beneficiaryDto.accessibility + ", ";
            sql += "majorProblem = " + beneficiaryDto.majorProblem + " ";
            sql += "WHERE id = " + beneficiaryDto.beneficiaryId;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool DeleteBeneficiary(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "UPDATE beneficiary SET deleted = 1 WHERE id = " + id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        private BeneficiaryDTO Resolve(List<String> item)
        {
            BeneficiaryDTO result = new BeneficiaryDTO();
            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.lastName = item[2];
            result.birthdate = DateTime.Parse(item[3]);
            result.email = item[4];
            result.phone = item[5];
            result.gender = Char.Parse(item[6]);
            result.dni = int.Parse(item[7]);
            result.addressId = int.Parse(item[8]);
            result.beneficiaryId = int.Parse(item[9]);
            result.destination = int.Parse(item[10]);
            result.ages = int.Parse(item[11]);
            result.health = int.Parse(item[12]);
            result.accessibility = int.Parse(item[13]);
            result.majorProblem = int.Parse(item[14]);
            return result;
        }
    }
}
