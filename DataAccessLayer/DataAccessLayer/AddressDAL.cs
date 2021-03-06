﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class AddressDAL
    {

        public AddressDTO GetAddress(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM address WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public bool SaveAddress(AddressDTO addressDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO address (street, number, apartment, neighborhood, comment, countryIso) VALUES (";
            sql += "'" + addressDto.street + "', ";
            sql += addressDto.number + ", ";
            sql += "'" + addressDto.apartment + "', ";
            sql += "'"+ addressDto.neighborhood + "', ";
            sql += "'" + addressDto.comment + "', ";
            sql += "'" + addressDto.countryIso + "'";
            sql += ");SELECT @@IDENTITY";
            addressDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public List<AddressDTO> GetAddresses()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<AddressDTO> result = new List<AddressDTO>();

            sql = "SELECT * FROM address";
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

        public bool UpdateAddress(AddressDTO addressDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "UPDATE address SET ";
            sql += "street = '" + addressDto.street + "',  ";
            sql += "number = " + addressDto.number + ", ";
            sql += "apartment = '" + addressDto.apartment + "',  ";
            sql += "neighborhood = '" + addressDto.neighborhood + "', ";
            sql += "comment = '" + addressDto.comment + "', ";
            sql += "countryIso = '" + addressDto.countryIso + "' ";
            sql += "WHERE id = " + addressDto.id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        private AddressDTO Resolve(List<String> item)
        {
            AddressDTO result = new AddressDTO();

            result.id = int.Parse(item[0]);
            result.street = item[1];
            result.number = int.Parse(item[2]);
            result.apartment = item[3];
            result.neighborhood = item[4];
            result.comment = item[5];
            result.countryIso = item[6];
            return result;
        }
    }
}
