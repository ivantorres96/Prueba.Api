using Microsoft.EntityFrameworkCore;
using Prueba.Api.DataAccess;
using Prueba.Api.DTOs.Universal;
using System.Net;

namespace Prueba.Api.Domains.State
{
    public class StateDomainGetStateList(DbContexts db) : IStateDomainGetStateList
    {
        public async Task<UniversalResponseDto<object>> GetState()
        {
            var response = new UniversalResponseDto<object>();
            try
            {
                var statesDb = await db.States.ToListAsync();
                response.Data = statesDb.Select(x => new { x.Id, x.Name });
                response.StatusCode = HttpStatusCode.OK.GetHashCode();
            }
            catch (Exception)
            {
                response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            }
            return response;
        }
    }

    public class StateDomainGetStateById(DbContexts db) : IStateDomainGetStateById
    {
        public async Task<UniversalResponseDto<object>> GetState(int id)
        {
            var response = new UniversalResponseDto<object>();
            try
            {
                var statesDb = await db.States.FirstOrDefaultAsync(state => state.Id.Equals(id));
                response.Data = statesDb;
                response.StatusCode = HttpStatusCode.OK.GetHashCode();
            }
            catch (Exception)
            {
                response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            }
            return response;
        }
    }
}
