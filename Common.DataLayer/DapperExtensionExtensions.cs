using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using DapperExtensions;
using DapperExtensions.Mapper;
using Dapper;
using System.Data;
using System.ComponentModel;

namespace Common.DataLayer
{
    public static class DapperExtensionExtensions
    {
        private const string ParameterResult = "@result";

        public static object GetPrimaryKey<T>(this IDapperExtensionsConfiguration configuration, T entity) where T : class
        {
            return configuration.GetPrimaryKeyPropertyInfo<T>().GetValue(entity);
        }

        public static PropertyInfo GetPrimaryKeyPropertyInfo<T>(this IDapperExtensionsConfiguration configuration) where T : class
        {
            IClassMapper map = configuration.GetMap<T>();
            IPropertyMap propertyMap = map.Properties.Single(p => p.KeyType != KeyType.NotAKey);
            return propertyMap.PropertyInfo;
        }

        /// <summary>
        /// map list childs to list parents
        /// </summary>
        /// <typeparam name="TParent"></typeparam>
        /// <typeparam name="TChild"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="parents"></param>
        /// <param name="children"></param>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <param name="action"></param>
        public static void ApplyParentChild<TParent, TChild, TId>(
            this IEnumerable<TParent> parents, IEnumerable<TChild> children,
            Func<TParent, TId> id, Func<TChild, TId> parentId,
            Action<TParent, TChild> action)
        {
            if (parents != null)
            {
                var lookup = parents.ToDictionary(id);
                if (children != null)
                {
                    TParent parent;
                    foreach (var child in children)
                    {
                        if (lookup.TryGetValue(parentId(child), out parent))
                        {
                            action(parent, child);
                        }
                    }
                }
            }
        }

        public static void AddReturnValueParameter(this DynamicParameters parameters)
        {
            parameters.Add(ParameterResult, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
        }

        public static T GetReturnValue<T>(this DynamicParameters parameters)
        {
            return parameters.Get<T>(ParameterResult);
        }

        public static void AddOutputParameter(this DynamicParameters parameters, string name, DbType dbType = DbType.Int32)
        {
            parameters.Add(name, dbType: dbType, direction: ParameterDirection.Output);
        }
    }

    public static class DataTableExtension
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data, Func<PropertyDescriptor, bool> AllowProperties = null)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                if (AllowProperties != null && !AllowProperties(prop))
                {
                    continue;
                }

                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (AllowProperties != null && !AllowProperties(prop))
                    {
                        continue;
                    }

                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            return table;
        }
    }
}
