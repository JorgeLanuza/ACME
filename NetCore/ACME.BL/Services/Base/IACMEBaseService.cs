using ACME.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.BL.Services.Base
{
    public interface IACMEBaseService<TDto, TId> where TDto : ACMEDto<TId>
    {
        Task<ACMECollectionServiceResult<TDto, TId>> GetAllAsync();
        Task<ACMECollectionServiceResult<TDto, TId>> GetAllNotDeletedAsync();
        Task<ACMEServiceResult<TDto, TId>> GetByIdAsync(TId id);
        Task<ACMEServiceResult<TDto, TId>> AddAsync(TDto dto);
        Task<ACMEServiceResult<TDto, TId>> UpdateAsync(TDto dto);
        Task<ACMEServiceResult<TDto, TId>> DeleteByIdAsync(TId id);
    }
}
