using System;
using Microsoft.AspNetCore.SignalR.Protocol;
using SqlAssistant.Services.Metadata;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
namespace SqlAssistant.Services.Generators
{
    public class SPGeneratorService
    {
        public string GenerateSelectSP(ProcedureReuest pr)

        {
            var currentStringBuilder = new StringBuilder();
            InitialConfig(pr, currentStringBuilder);
            currentStringBuilder.AppendLine($"   SELECT");
            for (int i = 0; i < pr.RequiredColumns.Count; i++)
            {
                var column = pr.RequiredColumns[i];
                var comma = i == pr.RequiredColumns.Count - 1 ? "" : ",";
                currentStringBuilder.AppendLine($"      {column}{comma}");
            }
            var mainAlias = GetAlias(pr.BaseTable);
            currentStringBuilder.AppendLine($"   FROM {pr.Schema}.{pr.BaseTable} {mainAlias}");
            //Joins
            JoinTables(currentStringBuilder, pr);

            //Where
            WhereClause(currentStringBuilder, pr, mainAlias);
            currentStringBuilder.AppendLine();
            currentStringBuilder.AppendLine("END;");
            currentStringBuilder.AppendLine("GO");
            return currentStringBuilder.ToString();
        }
        public string GenerateInsertSP(ProcedureReuest pr)
        {
            StringBuilder stringBuilder = new StringBuilder();

            InitialConfig(pr, stringBuilder);
            stringBuilder.AppendLine($"   INSERT INTO {pr.BaseTable} ");
            stringBuilder.AppendLine("(");
            for (int i = 0; i < pr.RequiredColumns.Count; i++)
            {
                var clmn = pr.RequiredColumns[i];
                var comma = i == pr.RequiredColumns.Count - 1 ? "" : ",";
                stringBuilder.AppendLine($"     {clmn}{comma}");
            }
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine("VALUES");
            stringBuilder.AppendLine("(");
            for (int i = 0; i < pr.RequiredColumns.Count; i++)
            {
                var clmn = pr.RequiredColumns[i];
                var comma = i == pr.RequiredColumns.Count - 1 ? "" : ",";
                stringBuilder.AppendLine($"     @{clmn}{comma}");
            }
            stringBuilder.AppendLine(");");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("END;");
            stringBuilder.AppendLine("GO");

            return stringBuilder.ToString();
        }
        public string GenerateUpdateSP(ProcedureReuest pr)
        {
            StringBuilder sb = new StringBuilder();
            InitialConfig(pr, sb);
            sb.AppendLine($"UPDATE {pr.BaseTable}");
            sb.AppendLine("SET");
            sb.AppendLine();
            return sb.ToString();
        }
        public string GenerateDeleteSP(ProcedureReuest pr)
        {
            StringBuilder sb = new StringBuilder();
            InitialConfig(pr, sb);
            return sb.ToString();
        }
        private StringBuilder InitialConfig(ProcedureReuest pr, StringBuilder stringBuilder)
        {
            var objectHeder = $"IF OBJECT_ID (N'{pr.Schema}.{pr.Name}') IS NOT NULL";
            var drop = $"   DROP PROCEDURE {pr.Schema}.{pr.Name};";
            stringBuilder.AppendLine(objectHeder);
            stringBuilder.AppendLine(drop);
            stringBuilder.AppendLine("GO");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("SET ANSI_NULLS ON;");
            stringBuilder.AppendLine("SET QUOTED_IDENTIFIER ON;");
            stringBuilder.AppendLine("GO");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(
                $"CREATE PROCEDURE {pr.Schema}.{pr.Name}");
            // Pamaraters are required
            for (int i = 0; i < pr.Parameters.Count; i++)
            {
                var param = pr.Parameters[i];
                var comma = i == pr.Parameters.Count - 1 ? "" : ",";
                stringBuilder.AppendLine($"     @{param}{comma}");

            }
            stringBuilder.AppendLine("AS");
            stringBuilder.AppendLine("BEGIN");
            stringBuilder.AppendLine("   SET NOCOUNT ON;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"/***************");
            stringBuilder.AppendLine($"{pr.AdditionalStandards}");
            stringBuilder.AppendLine($"***************/");
            stringBuilder.AppendLine();
            return stringBuilder;
        }
        private void JoinTables(StringBuilder sb, ProcedureReuest pr)
        {
            if (pr.Joins == null || pr.Joins.Count == 0)
            {
                return;
            }
            foreach (var join in pr.Joins)
            {
                var leftAlias = GetAlias(join.LeftTable);
                var rightAlias = GetAlias(join.RightTable);
                sb.AppendLine($"   {join.JoinType} JOIN " +
                              $"{pr.Schema}.{join.RightTable} {rightAlias}");
                sb.AppendLine($"    ON " + $"{leftAlias}.{join.LeftColumn} = " +
                                           $"{rightAlias}.{join.RightColumn};");
            }


        }
        private string GetAlias(string table)
        {
            return string.Concat(table.Split('_').Select(x => x[0])).ToLower();
        }
        private void WhereClause(StringBuilder sb, ProcedureReuest pr, string alias)
        {
            if (pr.Filters == null || pr.Filters.Count == 0)
            {
                return;
            }
            sb.AppendLine();
            sb.AppendLine("WHERE");
            for (int i = 0; i < pr.Filters.Count; i++)
            {
                var filter = pr.Filters[i];
                var and = i == pr.Filters.Count - 1 ? "" : "AND";
                sb.AppendLine($@" ( {filter.Value} IS NULL OR {alias}.{filter.Column} {filter.Operator} {filter.Value} ) {and}");
            }
        }
    }
}