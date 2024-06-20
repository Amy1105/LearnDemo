using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Services
{
    /// <summary>
    /// EFCORE 查询
    /// </summary>
    internal class SearchClass
    {
        private readonly SchoolContext context;
        public SearchClass(SchoolContext _context)
        {
            context = _context;
        }


        /// <summary>
        /// 【字段类型】，【字段名称】，【主外键】，【注释，是否为null，默认值】
        /// </summary>
        public void GetTableInfoForMermaid()
        {
            var model = context.Model;
            // 获取所有实体类型
            foreach (var entityType in model.GetEntityTypes())
            {
                // 打印表名
                Console.WriteLine("Table Name: " + entityType.GetTableName());
                if(!"OutOrderHeaders".Equals(entityType.GetTableName()))
                {
                    continue;
                }
                // 获取所有属性
                foreach (var property in entityType.GetProperties())
                {
                    // 打印属性名和数据类型
                    object s = "";
                    property.TryGetDefaultValue(out s);
                    var defaultVal = s == null ? "" : s.ToString();
                    var p=property.GetFunctionColumnMappings();
                    var commentName= RelationalAnnotationNames.Comment;
                    var ss=  property.FindAnnotation(commentName);

                    var columnName = RelationalAnnotationNames.ColumnName;
                    var ColumnNameStr = property.FindAnnotation(columnName);
                   

                    var storeType = RelationalAnnotationNames.StoreType;
                    var storeTypeStr = property.FindAnnotation(storeType);

                    var storeTypeStr2 = property.FindRelationalTypeMapping()?.StoreType;

                    IEntityType entityType2 = model.FindEntityType(typeof(OutOrderHeader));
                    var property2 = entityType2.FindProperty("ID");
                   // var columnComment = property2.GetColumnComment();

                    Console.WriteLine($"Column Name: {property.Name}, Data Type: {property.ClrType},sqlserver:{property.GetColumnType()},isBullable:{property.IsColumnNullable()},default:{defaultVal},comment:");
                }

                // 获取主键
                var primaryKey = entityType.FindPrimaryKey();
                if (primaryKey != null)
                {
                    // 打印主键属性
                    Console.WriteLine("Primary Key Properties: ");
                    foreach (var property in primaryKey.Properties)
                    {
                        Console.WriteLine($"- {property.Name}");
                    }
                }

                // 获取外键
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    // 打印外键信息
                    Console.WriteLine($"Foreign Key From: {foreignKey.PrincipalEntityType.Name} to {foreignKey.DeclaringEntityType.Name}");
                    foreach (var property in foreignKey.Properties)
                    {
                        Console.WriteLine($"- {property.Name}");
                    }
                }

                // 获取索引
                foreach (var index in entityType.GetIndexes())
                {
                    // 打印索引信息
                    Console.WriteLine($"Index Name: {index.Name}");
                    foreach (var property in index.Properties)
                    {
                        Console.WriteLine($"- {property.Name}");
                    }
                }
            }
        }
    }
}
