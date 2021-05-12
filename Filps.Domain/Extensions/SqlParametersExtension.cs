using System;
using System.Collections.Generic;
using System.Linq;
using Filps.Domain.Helpers.Attributes;
using Filps.Domain.Helpers.Converters;
using Microsoft.Data.SqlClient;

namespace Filps.Domain.Extensions
{
    public static class SqlParameterExtensions
    {
        private class QueryParamInfo
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }

        public static object[] ToSqlParamsArray(this object obj, SqlParameter[] additionalParams = null)
        {
            var result = ToSqlParamsList(obj, additionalParams);
            return result.ToArray<object>();
        }

        private static IEnumerable<SqlParameter> ToSqlParamsList(this object obj, SqlParameter[] additionalParams = null)
        {
            var props = (
                from p in obj.GetType().GetProperties()
                let nameAttr = p.GetCustomAttributes(typeof(QueryParamNameAttribute), true)
                let ignoreAttr = p.GetCustomAttributes(typeof(QueryParamIgnoreAttribute), true)
                select new {Property = p, Names = nameAttr, Ignores = ignoreAttr}).ToList();

            var result = new List<SqlParameter>();

            props.ForEach(p =>
            {
                if (p.Ignores != null && p.Ignores.Length > 0)
                    return;

                var name = p.Names.FirstOrDefault() as QueryParamNameAttribute;
                var pinfo = new QueryParamInfo();

                if (name != null && !String.IsNullOrWhiteSpace(name.Name))
                    pinfo.Name = name.Name.Replace("@", "");
                else
                    pinfo.Name = p.Property.Name.Replace("@", "");

                pinfo.Value = p.Property.GetValue(obj) ?? DBNull.Value;
                var sqlParam = new SqlParameter(pinfo.Name, TypeConvertor.ToSqlDbType(p.Property.PropertyType))
                {
                    Value = pinfo.Value
                };

                result.Add(sqlParam);
            });

            if (additionalParams != null && additionalParams.Length > 0)
                result.AddRange(additionalParams);

            return result;
        }
    }
}