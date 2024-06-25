namespace LearnAutomapper.Dto
{
    public class InstructorDto
    {
        public int ID { get; set; }

        public string? LastName { get; set; }

        public string? FirstMidName { get; set; }

        public List<DepartmentDto> Departments { get; set; }
    }

}
