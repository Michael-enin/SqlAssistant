using Dapper;
using Microsoft.Data.SqlClient;

namespace SqlAssistant.Services.Metadata
{
    public class RelationshipService
    {
        private readonly IConfiguration _config;
        public RelationshipService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<List<RelationshipMetadata>> GetRelationshipsAsync(string schema, string table)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var sql = @"
            SELECT 
                fk.name AS BaseTable,
                tp.name AS BaseColumn,
                cp.name AS ReferenceTable,
                tf.name AS ReferenceColumn
            FROM sys.foreign_keys fk
            INNER JOIN sys.foreign_key_columns fkc 
                ON fk.object_id = fkc.constraint_object_id
            INNER JOIN sys.tables tp 
                ON fk.referenced_object_id = tp.object_id
            INNER JOIN sys.columns cp 
                ON fkc.referenced_column_id = cp.column_id 
                AND cp.object_id = tp.object_id
            INNER JOIN sys.tables tf 
                ON fk.parent_object_id = tf.object_id
            INNER JOIN sys.columns cf 
                ON fkc.parent_column_id = cf.column_id 
                AND cf.object_id = tf.object_id
            WHERE tf.schema_id = SCHEMA_ID(@schema) 
            AND tf.name = @table";
            var result = await connection.QueryAsync<RelationshipMetadata>(sql, new { schema, table });
            return result.ToList();
        }
    }
}
