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
    public class VisitService : ACMEBaseService<VisitDto, VisitEntity, Guid>, IVisitService, IACMEBaseService<VisitDto, Guid>
    {
        private readonly ACMEBaseValidation<VisitDto> _ACMEValidation;

        protected new readonly ILogger<VisitService> _logger;

        private new readonly IMapper _mapper;

        private readonly VisitJsonRepository _jsonRepository;

        private new VisitRepository Repository { get; }

        public VisitService(ILogger<VisitService> logger, VisitRepository repository, VisitJsonRepository jsonRepository, IMapper mapper)
            : base(logger, (ACMERepository<VisitEntity, Guid>)repository, (IMapper)mapper, (ACMEBaseValidation<VisitDto>)null)
        {
            Repository = repository;
            _jsonRepository = jsonRepository;
            _mapper = mapper;
            _logger = logger;
        }
        #region Database Actions
        public new async Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllAsync()
        {
            ACMECollectionServiceResult<VisitDto, Guid> ACMECollectionServiceResult = new()
            {
                ResultObject = new DtoCollectionResult<VisitDto, Guid>()
            };
            try
            {
                IEnumerable<VisitEntity> allNotDeleted = await Repository.GetAllAsync();
                ACMECollectionServiceResult.ResultObject.Items = _mapper.Map<IList<VisitDto>>(allNotDeleted)!;
                ACMECollectionServiceResult.ResultObject.CurrentPage = 1;
                ACMECollectionServiceResult.ResultObject.PageCount = 1;
                ACMECollectionServiceResult.ResultObject.PageSize = allNotDeleted.Count();
                ACMECollectionServiceResult.ResultObject.RowCount = allNotDeleted.Count();
                _logger.LogDebug(JsonConvert.SerializeObject(ACMECollectionServiceResult.ResultObject));
                return ACMECollectionServiceResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting visits from database", ex);
            }
        }
        public new async Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllNotDeletedAsync()
        {
            ACMECollectionServiceResult<VisitDto, Guid> ACMECollectionServiceResult = new()
            {
                ResultObject = new DtoCollectionResult<VisitDto, Guid>()
            };
            try
            {
                IEnumerable<VisitEntity> allNotDelleted = await Repository.GetAllNotDeletedAsync();
                ACMECollectionServiceResult.ResultObject.Items = _mapper.Map<IList<VisitDto>>(allNotDelleted)!;
                ACMECollectionServiceResult.ResultObject.CurrentPage = 1;
                ACMECollectionServiceResult.ResultObject.PageCount = 1;
                ACMECollectionServiceResult.ResultObject.PageSize = allNotDelleted.Count();
                ACMECollectionServiceResult.ResultObject.RowCount = allNotDelleted.Count();
                _logger.LogDebug(JsonConvert.SerializeObject(ACMECollectionServiceResult.ResultObject));
                return ACMECollectionServiceResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting visits from database", ex);
            }
        }
        public new async Task<ACMEServiceResult<VisitDto, Guid>> GetByIdAsync(Guid selectedVisitId)
        {
            try
            {
                VisitEntity? entity = await Repository.GetByIdAsync(selectedVisitId);
                if (entity == null)
                    return null;
                return new ACMEServiceResult<VisitDto, Guid> { ResultObject = _mapper.Map<VisitDto>(entity) };
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting visit from database", ex);
            }
        }
        public new async Task<ACMEServiceResult<VisitDto, Guid>> AddAsync(VisitDto newVisit)
        {
            ACMEServiceResult<VisitDto, Guid> ACMEServiceResult = new()
            {
                Validation = _ACMEValidation.Validate(newVisit)
            };
            try
            {
                if (ACMEServiceResult.Validation.IsValid)
                {
                    VisitEntity entity = _mapper.Map<VisitEntity>(newVisit);
                    await Repository.AddAsync(entity);
                    ACMEServiceResult.ResultObject = _mapper.Map<VisitDto>(entity);
                }
                _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
                return ACMEServiceResult;

            }
            catch (Exception ex)
            {
                throw new Exception("Error adding a visit to database", ex);
            }
        }
        public new async Task<ACMEServiceResult<VisitDto, Guid>> UpdateAsync(VisitDto currentVisitDto)
        {
            try
            {
                VisitEntity? visitEntity = await Repository.GetByIdAsync(currentVisitDto.Id);
                if (visitEntity == null)
                    return null;
                _mapper.Map(currentVisitDto, visitEntity);
                ACMEServiceResult<VisitDto, Guid> ACMEServiceResult = new()
                {
                    Validation = _ACMEValidation.Validate(_mapper.Map<VisitDto>(visitEntity))
                };
                if (ACMEServiceResult.Validation.IsValid)
                {
                    ACMEServiceResult.ResultObject = _mapper.Map<VisitDto>(Repository.UpdateAsync(visitEntity));
                }
                _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
                return ACMEServiceResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error editting visits from database", ex);
            }
        }
        public new async Task<ACMEServiceResult<VisitDto, Guid>> DeleteByIdAsync(Guid selectedVisitId)
        {
            try
            {
                if (selectedVisitId == null)
                    return null;
                await Repository.DeleteAsync(selectedVisitId);
                ACMEServiceResult<VisitDto, Guid> ACMEServiceResult = new();
                _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
                return ACMEServiceResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deletting visits from database", ex);
            }
        }
        #endregion Database Actions
        #region Json Actions
        public async Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllFromJsonAsync()
        {
            ACMECollectionServiceResult<VisitDto, Guid> ACMECollectionServiceResult = new()
            {
                ResultObject = new DtoCollectionResult<VisitDto, Guid>()
            };
            try
            {
                IEnumerable<VisitEntity> visitEntities = await _jsonRepository.GetAllFromJsonAsync();
                ACMECollectionServiceResult.ResultObject.Items = _mapper.Map<List<VisitDto>>(visitEntities)!;
                ACMECollectionServiceResult.ResultObject.CurrentPage = 1;
                ACMECollectionServiceResult.ResultObject.PageCount = 1;
                ACMECollectionServiceResult.ResultObject.PageSize = visitEntities.Count();
                ACMECollectionServiceResult.ResultObject.RowCount = visitEntities.Count();
                _logger.LogDebug(JsonConvert.SerializeObject(ACMECollectionServiceResult.ResultObject));
                return ACMECollectionServiceResult;

            }
            catch (Exception ex)
            {
                throw new Exception("Error getting visits from Json", ex);
            }
        }
        public async Task<ACMECollectionServiceResult<VisitDto, Guid>> GetAllNotDeletedFromJsonAsync()
        {
            ACMECollectionServiceResult<VisitDto, Guid> ACMECollectionServiceResult = new()
            {
                ResultObject = new DtoCollectionResult<VisitDto, Guid>()
            };
            try
            {
                IEnumerable<VisitEntity> visitEntities = await _jsonRepository.GetAllVisitsNotDeletedFromJsonAsync();
                ACMECollectionServiceResult.ResultObject.Items = _mapper.Map<List<VisitDto>>(visitEntities)!;
                ACMECollectionServiceResult.ResultObject.CurrentPage = 1;
                ACMECollectionServiceResult.ResultObject.PageCount = 1;
                ACMECollectionServiceResult.ResultObject.PageSize = visitEntities.Count();
                ACMECollectionServiceResult.ResultObject.RowCount = visitEntities.Count();
                _logger.LogDebug(JsonConvert.SerializeObject(ACMECollectionServiceResult.ResultObject));
                return ACMECollectionServiceResult;

            }
            catch (Exception ex)
            {
                throw new Exception("Error getting visits from Json", ex);
            }
        }
        public async Task<ACMEServiceResult<VisitDto, Guid>> AddToJsonAsync(VisitDto newVisit)
        {
            ACMEServiceResult<VisitDto, Guid> ACMEServiceResult = new()
            {
                Validation = _ACMEValidation.Validate(newVisit)
            };
            try
            {
                if (ACMEServiceResult.Validation.IsValid)
                {
                    VisitEntity entity = _mapper.Map<VisitEntity>(newVisit);
                    await _jsonRepository.AddToJsonAsync(entity);
                    ACMEServiceResult.ResultObject = _mapper.Map<VisitDto>(entity);
                }
                _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
                return ACMEServiceResult;

            }
            catch (Exception ex)
            {
                throw new Exception("Error adding a visit to Json", ex);
            }
        }
        public async Task<ACMEServiceResult<VisitDto, Guid>> GetByIdFromJsonAsync(Guid selectedVisitId)
        {
            try
            {
                VisitEntity? entity = await _jsonRepository.GetByIdAsync(selectedVisitId);
                if (entity == null)
                    return null;
                return new ACMEServiceResult<VisitDto, Guid> { ResultObject = _mapper.Map<VisitDto>(entity) };
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting visit from Json", ex);
            }
        }
        public async Task<bool> DeleteFromJsonByIdAsync(Guid selectedVisitId)
        {
            try
            {
                bool response = await _jsonRepository.DeleteFromJsonAsync(selectedVisitId);
                if (response)
                {
                    ACMEServiceResult<VisitDto, Guid> ACMEServiceResult = new();
                    _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deletting visits from Json", ex);
            }
            return false;
        }
        public async Task<ACMEServiceResult<VisitDto, Guid>> UpdateFromJsonAsync(VisitDto currentVisitDto)
        {
            try
            {
                VisitEntity? visitEntity = await _jsonRepository.GetByIdAsync(currentVisitDto.Id);
                if (visitEntity == null)
                    return null;
                _mapper.Map(currentVisitDto, visitEntity);
                ACMEServiceResult<VisitDto, Guid> ACMEServiceResult = new()
                {
                    Validation = _ACMEValidation.Validate(_mapper.Map<VisitDto>(visitEntity))
                };
                if (ACMEServiceResult.Validation.IsValid)
                {
                    ACMEServiceResult.ResultObject = _mapper.Map<VisitDto>(_jsonRepository.UpdateInJsonAsync(visitEntity));
                }
                _logger.LogDebug(JsonConvert.SerializeObject(ACMEServiceResult.ResultObject));
                return ACMEServiceResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error editting visits from Json", ex);
            }
        }
        #endregion Json Actions
    }
}