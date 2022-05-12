using SqlSugar;

namespace model
{
    [SugarTable("gg")]
    public class gg
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "Id")]
        public int Id { get; set; }
    }
}