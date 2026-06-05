using Dapper;
using Microsoft.Data.SqlClient;

public class MetadataService
{
    private readonly IConfiguration _config;
    public MetadataService(IConfiguration config)
    {
        _config = config;
    }
    public async Task<List<ColumnMetadata>> GetColumneAsync(string schema, string table)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        var sql = @"
        SELECT 
            c.Column_Name AS ColumnName,
            c.DATA_TYPE AS DataType
         FROM INFROMATION_SCHEMA.COLUMNS c
         WHERE c.TABLE_SCHEMA = @schema
         AND c.TABLE_NAME = @table";
        var result = await connection.QueryAsync<ColumnMetadata>(sql, new { schema, table });
        return result.ToList(); ;
    }
}