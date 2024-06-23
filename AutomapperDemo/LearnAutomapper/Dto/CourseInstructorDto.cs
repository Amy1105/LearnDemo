namespace LearnAutomapper.Dto
{
    public class CourseInstructorDto
    {
        public int ID { get; set; }

        public string? LastName { get; set; }

        public string? FirstMidName { get; set; }

        public List<CourseInstructorDepartmentDto> Departments { get; set; }
    }

}
