namespace ACME.BL
{
    using ACME.BL.Services.Base;
    using ACME.Dtos;
    public interface IVisitService : IACMEBaseService<VisitDto, Guid>
    {
        new Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllAsync();
        new Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllNotDeletedAsync();
        new Task<ACMEServiceResult<VisitDto, Guid>> GetByIdAsync(Guid id);
        new Task<ACMEServiceResult<VisitDto, Guid>> AddAsync(VisitDto dto);
        new Task<ACMEServiceResult<VisitDto, Guid>> UpdateAsync(VisitDto dto);
        new Task<ACMEServiceResult<VisitDto, Guid>> DeleteByIdAsync(Guid id);
        Task<bool> DeleteFromJsonByIdAsync(Guid id);
        Task<ACMEServiceResult<VisitDto, Guid>> AddToJsonAsync(VisitDto id);
        Task<ACMEServiceResult<VisitDto, Guid>> GetByIdFromJsonAsync(Guid id);
        Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllFromJsonAsync();
        Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllNotDeletedFromJsonAsync();
        Task<ACMEServiceResult<VisitDto, Guid>> UpdateFromJsonAsync(VisitDto dto);
    }
}
    