using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Nokody.API.Data
{
    public static class DataTableExtensions
    {
        public static DataTable ToDataTable<T>(this object obj, Dictionary<string, PropertyInfo> props, IEnumerable<T> dataItems) where T : class, new()
        {
            var table = new DataTable();

            foreach (var prop in props)
            {
                table.Columns.Add(new DataColumn(prop.Key, Nullable.GetUnderlyingType(prop.Value.PropertyType) ?? prop.Value.PropertyType));
            }

            foreach (var item in dataItems)
            {
                var dataRow = table.NewRow();

                foreach (var prop in props)
                {
                    var val = prop.Value.GetValue(item);
                    if (val != null)
                        dataRow[prop.Key] = val;
                }
                table.Rows.Add(dataRow);
            }

            return table;
        }
        public static IDictionary<string, DataTable> MapDataSetTableNames(DataSet dataset)
        {
            var result = new Dictionary<string, DataTable>();
            for (var i = 0; i < dataset.Tables[0].Columns.Count; i++)
            {

                var dataTable = dataset.Tables[i + 1];
                result[dataset.Tables[0].Rows[0][i].ToString()] = dataTable;
            }
            return result;
        }

        /// <summary>
        ///     Fills the public properties of a class from the first row of a DataTable
        ///     where the name of the property matches the column name from that DataTable.
        /// </summary>
        /// <param name="table">A DataTable that contains the data.</param>
        /// <param name="props">Contains Class properties</param>
        /// <returns>
        ///     A class of type T with its public properties matching column names
        ///     set to the values from the first row in the DataTable.
        /// </returns>
        public static T ToObject<T>(this DataTable table, Dictionary<string, PropertyInfo> props) where T : class, new()
        {
            var result = new T();
            if (table.HasRows())
                result = FillProperties<T>(table.Rows[0], props);
            return result;
        }

        /// <summary>
        ///     Fills the public properties of a class from each row of a DataTable where the name of
        ///     the property matches the column name in the DataTable, returning a List of T.
        /// </summary>
        /// <param name="dataTable">A DataTable that contains the data.</param>
        /// <param name="props">Contain Class Properties</param>
        /// <returns>
        ///     A List class T with each class's public properties matching column names
        ///     set to the values of a diffrent row in the DataTable.
        /// </returns>
        public static List<T> ToObjectList<T>(this DataTable dataTable, Dictionary<string, PropertyInfo> props)
            where T : class, new()
        {
            var result = new List<T>();
            if (!dataTable.HasRows()) return result;

            var items = from DataRow row in dataTable.Rows select FillProperties<T>(row, props);
            result.AddRange(items);
            return result;
        }

        public static Dictionary<TKeyType, T> ToObjectList<T, TKeyType>(this DataTable dataTable,
            Dictionary<string, PropertyInfo> props, string keyColumn) where T : class, new()
        {
            var result = new Dictionary<TKeyType, T>();
            if (!dataTable.HasRows()) return result;


            foreach (DataRow row in dataTable.Rows)
            {
                var obj = FillProperties<T>(row, props);
                result[(TKeyType)row[keyColumn]] = obj;
            }
            return result;
        }

        /// <summary>
        ///     Fills the public properties of a class from a DataRow where the name
        ///     of the property matches a column name from that DataRow.
        /// </summary>
        /// <param name="row">A DataRow that contains the data.</param>
        /// <param name="props"></param>
        /// <returns>
        ///     A class of type T with its public properties set to the
        ///     data from the matching columns in the DataRow.
        /// </returns>
        public static T FillProperties<T>(DataRow row, Dictionary<string, PropertyInfo> props) where T : class, new()
        {
            var result = new T();
            var classType = typeof(T);

            if (row.Table.Columns.Count < 1 || classType.GetProperties().Length < 1 || row.ItemArray.Length < 1)
                return result;

            foreach (DataColumn column in row.Table.Columns)
            {
                if (row[column] == DBNull.Value)
                    continue;

                var property = props.FirstOrDefault(_ => _.Key.ToLower() == column.ColumnName.ToLower()).Value;
                if (property != null)
                {
                    // If type is of type System.Nullable, do not attempt to convert the value
                    var newValue = IsNullable(property.PropertyType)
                        ? row[property.Name]
                        : Convert.ChangeType(row[column], property.PropertyType);

                    property.SetValue(result, newValue, null);
                }
            }
            return result;
        }

        private static bool HasRows(this DataTable dataTable)
        {
            return dataTable?.Columns.Count > 0;
        }

        private static bool IsNullable(Type type)
        {
            if (!type.IsValueType) return true; // ref-type
            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}