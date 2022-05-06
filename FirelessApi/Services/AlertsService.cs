using FirelessApi.Domain;
using FirelessApi.Domain.Repository;
using FirelessApi.Domain.Services;

namespace FirelessApi.Services;

public class AlertsService : IAlertsService
{
    private readonly IRepository<Alert> _alertsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AlertsService(IRepository<Alert> alertsRepository, IUnitOfWork unitOfWork)
    {
        _alertsRepository = alertsRepository;
        _unitOfWork = unitOfWork;
    }
    
    IRepository<Alert> ICrudService<Alert>.GetRepository() => _alertsRepository;
    IUnitOfWork ICrudService<Alert>.GetUnitOfWork() => _unitOfWork;

    void ICrudService<Alert>.UpdateMappingLogic(Alert existingEntity, Alert incomingEntity)
    {
        existingEntity.Id = incomingEntity.Id;
        existingEntity.Region = incomingEntity.Region;
        existingEntity.Data = incomingEntity.Data;
        existingEntity.Users = incomingEntity.Users;
    }

    public async Task<Response<Alert>> Update(Alert entity)
    {
        var updatedEntity = await _alertsRepository.UpdateAsync(entity);
        return new Response<Alert>(updatedEntity);
    }
}