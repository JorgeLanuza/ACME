using ACME.Dtos.Base_Dto.Authentication;

namespace ACME.Dtos
{
    public class EmployeeDto : UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserId { get; set; }
        public List<VisitDto> Visits { get; set; }
    }
}
