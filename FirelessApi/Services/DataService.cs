using FirelessApi.Domain;
using FirelessApi.Domain.Repository;
using FirelessApi.Domain.Services;

namespace FirelessApi.Services;

public class DataService : IDataService
{
    private readonly IRepository<Data> _dataRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DataService(IRepository<Data> dataRepository, IUnitOfWork unitOfWork)
    {
        _dataRepository = dataRepository;
        _unitOfWork = unitOfWork;
    }
    
    IRepository<Data> ICrudService<Data>.GetRepository() => _dataRepository;
    IUnitOfWork ICrudService<Data>.GetUnitOfWork() => _unitOfWork;

    void ICrudService<Data>.UpdateMappingLogic(Data existingEntity, Data incomingEntity)
    {
        existingEntity.Id = incomingEntity.Id;
        existingEntity.AtmCo = incomingEntity.AtmCo;
        existingEntity.Temperature = incomingEntity.Temperature;
        existingEntity.Humidity = incomingEntity.Humidity;
        existingEntity.BarometicPressure = incomingEntity.BarometicPressure;
        existingEntity.Alerts = incomingEntity.Alerts;
    }

    public async Task<Response<Data>> Update(Data entity)
    {
        var updatedEntity = await _dataRepository.UpdateAsync(entity);
        return new Response<Data>(updatedEntity);
    }
}