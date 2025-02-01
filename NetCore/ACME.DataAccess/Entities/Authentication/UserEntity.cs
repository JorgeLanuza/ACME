namespace ACME.DataAccess.Entities.Authentication
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("SEC_USERS")]
    public class UserEntity : ACMEBaseEntity<Guid>
    {
        [Column("AUTHENTICATION_ID")]
        public string AuthenticationId { get; set; }
        public virtual AuthenticationEntity Authentication { get; set; }

        [Column("LOCKED")]
        public bool? Locked { get; set; }

        [Column("LOCKED_DATE")]
        public DateTime? LockedDate { get; set; }

        [Column("DISABLED")]
        public bool Disabled { get; set; }

        [Column("CREATION_DATE")]
        public DateTime CreationDate { get; set; }

        [Column("VIOLATIONS_COUNTER")]
        public uint ViolationsCounter { get; set; }

        public virtual UserProfileEntity Profile { get; set; }
    }
}
