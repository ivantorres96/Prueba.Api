using Prueba.Api.DTOs.Universal;

namespace Prueba.Api.Domains.State
{
    public interface IStateDomainGetStateList
    {
        Task<UniversalResponseDto<object>> GetState();
    }

    public interface IStateDomainGetStateById
    {
        Task<UniversalResponseDto<object>> GetState(int Id);
    }
}
