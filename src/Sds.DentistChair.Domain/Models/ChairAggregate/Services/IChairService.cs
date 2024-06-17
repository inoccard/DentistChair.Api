using Sds.DentistChair.Domain.Models.ChairAggregate.Dtos;
using Sds.DentistChair.Domain.Models.ChairAggregate.Entities;

namespace Sds.DentistChair.Domain.Models.ChairAggregate.Services;

public interface IChairService
{
    public IQueryable<Chair> GetChairs();
    public Chair GetChair(long id);
    public Task<bool> SaveChair(Chair chair);
    public Task<bool> UpdateChair(Chair chair);
    public Task<bool> DeleteChair(long id);
    public IQueryable<Allocation> GetAllocations();
    public Task<Allocation> GetAllocation(long id);
    public Task<bool> Allocate(AllocationRequest request, Chair[] chairs);

}
