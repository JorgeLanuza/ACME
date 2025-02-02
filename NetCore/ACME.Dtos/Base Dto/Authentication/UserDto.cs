namespace ACME.Dtos.Base_Dto.Authentication
{
    using System;
    public class UserDto : ACMEDto<Guid>
    {
        public Guid AuthenticationId { get; set; }
        public AuthenticationDto Authentication { get; set; }
        public bool Locked { get; set; }
        public DateTime? LockDate { get; set; }
        public bool Disabled { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public uint ViolationCounter { get; set; }
        public virtual UserProfileDto Profile { get; set; }
    }
}
