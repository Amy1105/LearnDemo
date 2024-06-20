using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Dto
{
    internal class CourseDto
    {
        public int CourseID { get; set; }

        public string Title { get; set; } = string.Empty;

        public int? Credits { get; set; }

        public List<InstructorDto> InstructorDtos { get; set; }=new List<InstructorDto>();
    }

    public class InstructorDto
    {
        public int?  ID { get; set; }

        public string LastName { get; set; } 

        public string FirstMidName { get; set; } 

    }

    public class OrderDto
    {
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
    }
}
