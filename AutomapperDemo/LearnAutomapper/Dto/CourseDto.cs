using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAutomapper.Dto
{
    public class CourseDto
    {
        public int CourseID { get; set; }

        public string Title { get; set; } = string.Empty;

        public int? Credits { get; set; }

        public List<InstructorDto> InstructorDtos { get; set; }
    }

}
