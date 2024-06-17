using Sds.DentistChair.Domain.Models.ChairAggregate.Dtos;
using Sds.DentistChair.Domain.Models.ChairAggregate.Entities;
using Sds.DentistChair.Domain.Repository;

namespace Sds.DentistChair.Domain.Models.ChairAggregate.Services;

public class ChairService(
    IRepository repository) : IChairService
{
    public IQueryable<Chair> GetChairs()
        => repository.QueryAsNoTracking<Chair>();

    public Chair GetChair(long id)
        => repository.QueryAsNoTracking<Chair>()
        .FirstOrDefault(c => c.Id == id);

    public async Task<bool> SaveChair(Chair chair)
    {
        await repository.AddAsync(chair);
        return await repository.CommitAsync();
    }

    public async Task<bool> UpdateChair(Chair chair)
    {
        repository.Update(chair);
        return await repository.CommitAsync();
    }

    public async Task<bool> DeleteChair(long id)
    {
        var chair = GetChair(id);
        if (chair == null)
            return false;

        repository.Remove(chair);
        return await repository.CommitAsync();
    }

    public IQueryable<Allocation> GetAllocations()
    {
        return repository.Query<Allocation>();
    }

    public Task<Allocation> GetAllocation(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Allocate(AllocationRequest request, Chair[] chairs)
    {
        // Remove any existing allocations within the requested time frame
        var existingAllocations = GetAllocations()
            .Where(a => a.StartTime < request.EndTime && a.EndTime > request.StartTime)
            .ToArray();

        foreach (var allocation in existingAllocations)
            repository.Remove(allocation);

        // Calculate how many allocations per chair
        int totalChairs = chairs.Length;
        int allocationsPerChair = (int)Math.Ceiling((request.EndTime - request.StartTime).TotalMinutes / totalChairs);

        List<Allocation> newAllocations = [];
        AddAllocation(request, chairs, totalChairs, allocationsPerChair, newAllocations);

        await repository.AddRange(newAllocations);
        return await repository.CommitAsync();
    }

    private static void AddAllocation(AllocationRequest request, Chair[] chairs, int totalChairs, int allocationsPerChair, List<Allocation> newAllocations)
    {
        for (int i = 0; i < totalChairs; i++)
        {
            var chair = chairs[i];
            DateTime currentTime = request.StartTime;

            while (currentTime < request.EndTime)
            {
                DateTime endTime = currentTime.AddMinutes(allocationsPerChair);
                if (endTime > request.EndTime)
                    endTime = request.EndTime;

                newAllocations.Add(new Allocation
                {
                    ChairId = chair.Id,
                    StartTime = currentTime,
                    EndTime = endTime
                });

                currentTime = endTime;
            }
        }
    }
}
