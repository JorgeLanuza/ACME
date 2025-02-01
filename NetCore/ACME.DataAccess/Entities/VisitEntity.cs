namespace ACME.DataAccess.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("VISIT")]
    public class VisitEntity : ACMEBaseEntity<Guid>
    {
        [Column("CLIENT_NAME")]
        public string ClientName { get; set; } = string.Empty;

        [Column("VISIT_DATE")]
        public DateTime VisitDate { get; set; }

        [Column("NOTES")]
        public string Notes { get; set; } = string.Empty;

        [Column("IS_DELETED")]
        public bool IsDeleted { get; set; } = false;

        [Column("USER_LAST_MODIFIED")]
        public string UserLastModified { get; set; } = string.Empty;

        [Column("DATE_LAST_MODIFIED")]
        public DateTime DateLastModified { get; set; }

        [Column("EMPLOYEE_ID")]
        public Guid EmployeeId { get; set; }
        public EmployeeEntity Employee { get; set; } = null!;

        [Column("LOGTIMESTAMP")]
        public DateTime LogTimestamp { get; set; }

    }
}
