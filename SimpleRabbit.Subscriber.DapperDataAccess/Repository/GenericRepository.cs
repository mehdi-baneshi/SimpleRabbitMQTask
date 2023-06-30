using Dapper;
using SimpleRabbit.Subscriber.Domain.Entities;
using SimpleRabbit.Subscriber.Domain.Interfaces.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.DapperDataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly string _tableName;
        private readonly DapperContext _context;
        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        public GenericRepository(DapperContext context)
        {
            _context = context;
            _tableName = typeof(T).Name+"s";
        }

        public async Task Add(T entity)
        {
            var insertQuery = GenerateInsertQuery();

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, entity);
            }
        }

        public async Task<List<T>> Get()
        {
            using (var connection = _context.CreateConnection())
            {
                var items=await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
                return items.ToList();
            }
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }
    }
}
