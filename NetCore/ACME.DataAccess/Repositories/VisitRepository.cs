namespace ACME.DataAccess.Repositories
{
    using ACME.DataAccess.Context;
    using ACME.DataAccess.Entities;
    public class VisitRepository : ACMERepository<VisitEntity, Guid>
    {
        public VisitRepository(ACMEContext context) : base(context) { }
    }
}
