namespace ACME.DataAccess.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("SEC_USER_PROFILE")]
    public class UserProfileEntity: ACMEBaseEntity<Guid>
    {
        [Column("FULL_NAME")]
        public string FullName { get; set; }

        [Column("DEPARTMENT")]
        public string Department { get; set; }

        [Column("TELEPHONE")]
        public string Telephone { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("MOBILE")]
        public string Mobile { get; set; }

        [Column("USER_ID")]
        public string UserId { get; set; }
    }
}
