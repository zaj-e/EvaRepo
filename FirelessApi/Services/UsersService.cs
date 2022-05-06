using FirelessApi.Domain;
using FirelessApi.Domain.Repository;
using FirelessApi.Domain.Services;

namespace FirelessApi.Services;

public class UsersService : IUsersService
{
    private readonly IRepository<User> _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UsersService(IRepository<User> usersRepository, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }
    
    IRepository<User> ICrudService<User>.GetRepository() => _usersRepository;
    IUnitOfWork ICrudService<User>.GetUnitOfWork() => _unitOfWork;

    void ICrudService<User>.UpdateMappingLogic(User existingEntity, User incomingEntity)
    {
        existingEntity.Id = incomingEntity.Id;
        existingEntity.Email = incomingEntity.Email;
        existingEntity.Password = incomingEntity.Password;
        existingEntity.ConfirmPassword = incomingEntity.ConfirmPassword;
        existingEntity.ConfirmEmail = incomingEntity.ConfirmEmail;
        existingEntity.Name = incomingEntity.Name;
        existingEntity.Alerts = incomingEntity.Alerts;
    }

    public async Task<Response<User>> Update(User entity)
    {
        var updatedEntity = await _usersRepository.UpdateAsync(entity);
        return new Response<User>(updatedEntity);
    }
}