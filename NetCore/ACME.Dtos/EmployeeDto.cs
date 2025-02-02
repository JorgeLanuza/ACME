using ACME.Dtos.Base_Dto.Authentication;

namespace ACME.Dtos
{
    public class EmployeeDto : UserDto
    {
        public List<VisitDto> Visits { get; set; }
    }
}
