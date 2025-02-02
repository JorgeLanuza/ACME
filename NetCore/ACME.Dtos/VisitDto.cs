namespace ACME.Dtos
{
    public class VisitDto : ACMEDto<Guid>
    {
        public string? ClientName { get; set; }
        public DateTime VisitDate { get; set; }
        public string? Notes { get; set; }
        public bool IsDeleted { get; set; }
        public string? UserLastModified { get; set; }
        public DateTime DateLastModified { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual EmployeeDto Employee { get; set; }
        public DateTime LogTimestamp { get; set; }
    }
}
