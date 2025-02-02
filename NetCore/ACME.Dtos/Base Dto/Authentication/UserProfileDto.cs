namespace ACME.Dtos.Base_Dto.Authentication
{
    public class UserProfileDto : ACMEDto<Guid>
    {
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserId { get; set; }
    }
}
