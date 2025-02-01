namespace ACME.BL.Services
{
    using ACME.BL;
    using ACME.BL.Services.Base;
    using ACME.BL.Validations;
    using ACME.DataAccess.Entities;
    using ACME.DataAccess.Repositories;
    using ACME.Dtos;
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System.Security.Cryptography;

    public class VisitService : ACMEBaseService<VisitDto, VisitEntity, Guid>, IVisitService, IACMEBaseService<VisitDto, Guid>
    {
        private readonly ACMEBaseValidation<VisitDto> _ACMEValidation;

        protected readonly ILogger<VisitService> _logger;

        private new readonly IMapper _mapper;

        private new VisitRepository Repository { get; }

        public VisitService(ILogger<VisitService> logger, VisitRepository repository, IMapper mapper) 
            : base(logger, (ACMERepository<VisitEntity, Guid>) repository, (IMapper)mapper, (ACMEBaseValidation<VisitDto>)null)
        {
            Repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllAsync()
        {
            ACMECollectionServiceResult<VisitDto, Guid> ACMECollectionServiceResult = new()
            {
                ResultObject = new DtoCollectionResult<VisitDto, Guid>()
            };
            IEnumerable<VisitEntity> allNotDeleted = await Repository.GetAllAsync();
            ACMECollectionServiceResult.ResultObject.Items = _mapper.Map<IList<VisitDto>>(allNotDeleted)!;
            ACMECollectionServiceResult.ResultObject.CurrentPage = 1;
            ACMECollectionServiceResult.ResultObject.PageCount = 1;
            ACMECollectionServiceResult.ResultObject.PageSize = allNotDeleted.Count();
            ACMECollectionServiceResult.ResultObject.RowCount = allNotDeleted.Count();
            _logger.LogDebug(JsonConvert.SerializeObject(ACMECollectionServiceResult.ResultObject));
            return ACMECollectionServiceResult;
        }
        public async Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllNotDeletedAsync()
        {
            ACMECollectionServiceResult<VisitDto, Guid> ACMECollectionServiceResult = new()
            {
                ResultObject = new DtoCollectionResult<VisitDto, Guid>()
            };
            IEnumerable<VisitEntity> allNotDelleted = await Repository.GetAllNotDeletedAsync();
            ACMECollectionServiceResult.ResultObject.Items = _mapper.Map<IList<VisitDto>>(allNotDelleted)!;
            ACMECollectionServiceResult.ResultObject.CurrentPage = 1;
            ACMECollectionServiceResult.ResultObject.PageCount = 1;
            ACMECollectionServiceResult.ResultObject.PageSize = allNotDelleted.Count();
            ACMECollectionServiceResult.ResultObject.RowCount = allNotDelleted.Count();
            _logger.LogDebug(JsonConvert.SerializeObject(ACMECollectionServiceResult.ResultObject));
            return ACMECollectionServiceResult;
        }
        public async Task<ACMEServiceResult<VisitDto, Guid>> GetByIdAsync(Guid id)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null)
                return null;
            return new ACMEServiceResult<VisitDto, Guid> { ResultObject = _mapper.Map<VisitDto>(entity) };
        }
        public async Task<ACMEServiceResult<VisitDto, Guid>> AddAsync(VisitDto dto)
        {
            ACMEServiceResult<VisitDto, Guid> ACMEServiceResult = new()
            {
                Validation = _ACMEValidation.Validate(dto)
            };
            if (ACMEServiceResult.Validation.IsValid)
            {
                VisitEntity entity = _mapper.Map<VisitEntity>(dto);
                await Repository.AddAsync(entity);
                ACMEServiceResult.ResultObject = _mapper.Map<VisitDto>(entity);
            }
            _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
            return ACMEServiceResult;
        }
        public async Task<ACMEServiceResult<VisitDto, Guid>> UpdateAsync(VisitDto dto)
        {
            VisitEntity? val = await Repository.GetByIdAsync(dto.Id);
            if (val == null)
                return null;
            _mapper.Map(dto, val);
            ACMEServiceResult<VisitDto, Guid> ACMEServiceResult = new()
            {
                Validation = _ACMEValidation.Validate(_mapper.Map<VisitDto>(val))
            };
            if (ACMEServiceResult.Validation.IsValid)
            {
                ACMEServiceResult.ResultObject = _mapper.Map<VisitDto>(Repository.UpdateAsync(val));
            }
            _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
            return ACMEServiceResult;
        }
        public async Task<ACMEServiceResult<VisitDto, Guid>> DeleteByIdAsync(Guid id)
        {
            if (id == null)
                return null;
            await Repository.DeleteAsync(id);
            ACMEServiceResult<VisitDto, Guid> ACMEServiceResult = new();
            _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
            return ACMEServiceResult;
        }
    }
}