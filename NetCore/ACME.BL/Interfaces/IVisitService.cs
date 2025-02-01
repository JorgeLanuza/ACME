namespace ACME.BL
{
    using ACME.BL.Services.Base;
    using ACME.Dtos;
    public interface IVisitService : IACMEBaseService<VisitDto, Guid>
    {
        Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllAsync();
        Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllNotDeletedAsync();
        Task<ACMEServiceResult<VisitDto, Guid>> GetByIdAsync(Guid id);
        Task<ACMEServiceResult<VisitDto, Guid>> AddAsync(VisitDto dto);
        Task<ACMEServiceResult<VisitDto, Guid>> UpdateAsync(VisitDto dto);
        Task<ACMEServiceResult<VisitDto, Guid>> DeleteByIdAsync(Guid id);
    }
}
    