namespace ACME.Dtos.Base_Dto.Authentication
{
    public class AuthenticationDto
    {
        public string AuthenticationId { get; set; }
        public string Password { get; set; }
        public DateTime PasswordCreationDate { get; set; }
    }
}
