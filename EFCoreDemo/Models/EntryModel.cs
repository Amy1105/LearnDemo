using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//CustomDestinationTableName  和 CustomSourceTableName 属性
namespace EFCoreDemo.Models
{
    /// <summary>
    /// db中没有，用来接收数据
    /// </summary>
    public class EntryModel
    {
        public int EntryId { get; set; }

        public string Name { get; set; } = null!;
    }

    /// <summary>
    /// db中有
    /// </summary>
    public class EntryModelArchive
    {
        [Key]
        public int EntryId { get; set; }

        public string Name { get; set; } = null!;
    }
}
