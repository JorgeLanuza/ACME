namespace ACME.BL.Services
{
    using ACME.BL;
    using ACME.BL.Services.Base;
    using ACME.BL.Validations;
    using ACME.DataAccess.Repositories;
    using ACME.Dtos;
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public abstract class ACMEBaseService<TDto, TEntity, TId> : IACMEBaseService<TDto, TId> where TDto : ACMEDto<TId> where TEntity : class, new()
    {
        private readonly ACMEBaseValidation<TDto> _ACMEValidation;

        protected readonly ILogger _logger;

        protected IMapper _mapper;
        protected ACMERepository<TEntity, TId> Repository { get; }

        protected ACMEBaseService(ILogger logger, ACMERepository<TEntity, TId> repository, IMapper mapper, ACMEBaseValidation<TDto> ACMEValidation)
        {
            Repository = repository;
            _ACMEValidation = ACMEValidation;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ACMECollectionServiceResult<TDto, TId>> GetAllAsync()
        {
            ACMECollectionServiceResult<TDto, TId> ACMECollectionServiceResult = new()
            {
                ResultObject = new DtoCollectionResult<TDto, TId>()
            };
            IEnumerable<TEntity> all = await Repository.GetAllAsync();
            ACMECollectionServiceResult.ResultObject.Items = _mapper.Map<IList<TDto>>(all)!;
            ACMECollectionServiceResult.ResultObject.CurrentPage = 1;
            ACMECollectionServiceResult.ResultObject.PageCount = 1;
            ACMECollectionServiceResult.ResultObject.PageSize = all.Count();
            ACMECollectionServiceResult.ResultObject.RowCount = all.Count();
            _logger.LogDebug(JsonConvert.SerializeObject(ACMECollectionServiceResult.ResultObject));
            return ACMECollectionServiceResult;
        }
        public async Task<ACMECollectionServiceResult<TDto, TId>> GetAllNotDeletedAsync()
        {
            ACMECollectionServiceResult<TDto, TId> ACMECollectionServiceResult = new()
            {
                ResultObject = new DtoCollectionResult<TDto, TId>()
            };
            IEnumerable<TEntity> allNotDelleted = await Repository.GetAllNotDeletedAsync();
            ACMECollectionServiceResult.ResultObject.Items = _mapper.Map<IList<TDto>>(allNotDelleted)!;
            ACMECollectionServiceResult.ResultObject.CurrentPage = 1;
            ACMECollectionServiceResult.ResultObject.PageCount = 1;
            ACMECollectionServiceResult.ResultObject.PageSize = allNotDelleted.Count();
            ACMECollectionServiceResult.ResultObject.RowCount = allNotDelleted.Count();
            _logger.LogDebug(JsonConvert.SerializeObject(ACMECollectionServiceResult.ResultObject));
            return ACMECollectionServiceResult;
        }
        public async Task<ACMEServiceResult<TDto, TId>> GetByIdAsync(TId id)
        {
            TEntity val = await Repository.GetByIdAsync(id);
            if (val == null)
            {
                return null;
            }
            ACMEServiceResult<TDto, TId> ACMEServiceResult = new ACMEServiceResult<TDto, TId>
            {
                ResultObject = _mapper.Map<TDto>(val),
                Validation = null
            };
            _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
            return ACMEServiceResult;
        }
        public async Task<ACMEServiceResult<TDto, TId>> AddAsync(TDto dto)
        {
            ACMEServiceResult<TDto, TId> ACMEServiceResult = new()
            {
                Validation = _ACMEValidation.Validate(dto)
            };
            if (ACMEServiceResult.Validation.IsValid)
            {
                TEntity entity = _mapper.Map<TEntity>(dto);
                await Repository.AddAsync(entity);
                ACMEServiceResult.ResultObject = _mapper.Map<TDto>(entity);
            }
            _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
            return ACMEServiceResult;
        }
        public async Task<ACMEServiceResult<TDto, TId>> UpdateAsync(TDto dto)
        {
            TEntity? val = await Repository.GetByIdAsync(dto.Id);
            if (val == null)
                return null;
            _mapper.Map(dto, val);
            ACMEServiceResult<TDto, TId> ACMEServiceResult = new()
            {
                Validation = _ACMEValidation.Validate(_mapper.Map<TDto>(val))
            };
            if (ACMEServiceResult.Validation.IsValid)
            {
                ACMEServiceResult.ResultObject = _mapper.Map<TDto>(Repository.UpdateAsync(val));
            }
            _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
            return ACMEServiceResult;
        }
        public async Task<ACMEServiceResult<TDto, TId>> DeleteByIdAsync(TId id)
        {
            if (id == null)
                return null;
            await Repository.DeleteAsync(id);
            ACMEServiceResult<TDto, TId> ACMEServiceResult = new();
            _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
            return ACMEServiceResult;
        }
    }
}
