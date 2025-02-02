namespace ACME.DataAccess.Entities
{
    using ACME.DataAccess.Entities.Authentication;
    using System.Collections.Generic;
    public class EmployeeEntity: UserEntity
    {
        public List<VisitEntity> Visits { get; set; } = [];
    }
}
