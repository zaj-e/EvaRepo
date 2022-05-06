using FirelessApi.Domain;
using FirelessApi.Domain.Repository;
using FirelessApi.Domain.Services;

namespace FirelessApi.Services;

public class RegionsService : IRegionsService
{
    private readonly IRepository<Region> _regionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegionsService(IRepository<Region> regionsRepository, IUnitOfWork unitOfWork)
    {
        _regionsRepository = regionsRepository;
        _unitOfWork = unitOfWork;
    }
    
    IRepository<Region> ICrudService<Region>.GetRepository() => _regionsRepository;
    IUnitOfWork ICrudService<Region>.GetUnitOfWork() => _unitOfWork;

    void ICrudService<Region>.UpdateMappingLogic(Region existingEntity, Region incomingEntity)
    {
        existingEntity.Id = incomingEntity.Id;
        existingEntity.Latitude = incomingEntity.Latitude;
        existingEntity.Longitude = incomingEntity.Longitude;
        existingEntity.Alerts = incomingEntity.Alerts;
    }

    public async Task<Response<Region>> Update(Region entity)
    {
        var updatedEntity = await _regionsRepository.UpdateAsync(entity);
        return new Response<Region>(updatedEntity);
    }
}