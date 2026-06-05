public class ColumnMetadata
{
    public string ColumnName { get; set; }
    public string DataType { get; set; }
    public bool IsNullable { get; set; }
    public bool IsPrimaryKey { get; set; }
    public bool IsIdentity { get; set; }
}