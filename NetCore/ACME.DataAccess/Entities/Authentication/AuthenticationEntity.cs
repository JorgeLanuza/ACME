namespace ACME.DataAccess.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("SEC_AUTHENTICATION")]
    public class AuthenticationEntity
    {
        [Key]
        [Column("AUTHENTICATION_ID")]
        public string AuthenticationId { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("PASSWORD_CREATION_DATE")]
        public DateTime PasswordCreationDate { get; set; }
    }
}
