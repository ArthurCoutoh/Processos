using MySql.Data.MySqlClient;

namespace processos.Configs;

public static class DAOHelper
{
    public static string GetString(MySqlDataReader reader, string column_name)
    {
        return reader.IsDBNull(reader.GetOrdinal(column_name))
            ? string.Empty
            : reader.GetString(column_name);
    }

    public static double GetDouble(MySqlDataReader reader, string column_name)
    {
        return reader.IsDBNull(reader.GetOrdinal(column_name))
            ? 0.0
            : reader.GetDouble(column_name);
    }

    public static DateTime? GetDateTime(MySqlDataReader reader, string column_name)
    {
        return reader.IsDBNull(reader.GetOrdinal(column_name))
            ? null
            : reader.GetDateTime(column_name);
    }

    public static bool IsNull(MySqlDataReader reader, string column_name)
    {
        return reader.IsDBNull(reader.GetOrdinal(column_name));
    }
}
