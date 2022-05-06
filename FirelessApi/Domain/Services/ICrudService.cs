using FirelessApi.Domain.Repository;

namespace FirelessApi.Domain.Services;

public interface ICrudService<T> where T : AuditEntity //class
{
    protected IRepository<T> GetRepository();
    protected IUnitOfWork GetUnitOfWork();
    public async Task<Response<T>> CreateAsync(T entity)
    {
        try
        {
            var createdEntity = await GetRepository().InsertAsync(entity);
            await GetUnitOfWork().CompleteAsync();
            return new Response<T>(createdEntity);
        }
        catch (Exception ex)
        {
            return new Response<T>(
                $"An error occurred while saving the {nameof(T)}: {ex.Message}");
        }
    }

    public async Task<Response<T>> SearchByIdAsync(int entityId)
    {
        var entity = await GetRepository().GetAsync(entityId);
        return entity is null ? new Response<T>($"{nameof(T)} not found") 
            : new Response<T>(entity);
    }

    protected void UpdateMappingLogic(T existingEntity, T incomingEntity);

    public async Task<Response<T>> Update(T entity)
    {
        const string entityName = nameof(T);
        var repository = GetRepository();
        var existingEntity = await repository.GetAsync(entity.Id);
        if (existingEntity is null)
        {
            return new Response<T>($"{entityName} not found");
        }

        UpdateMappingLogic(existingEntity,  entity);
        
        try
        {
            await repository.UpdateAsync(existingEntity);
            await GetUnitOfWork().CompleteAsync();
            return new Response<T>(existingEntity);
        }
        catch (Exception ex)
        {
            return new Response<T>($"An error occurred while updating the {entityName}: {ex.Message}");
        }
    }
    
    
    
    public async Task<string> DeleteByIdAsync(int entityId)
    {
        var repository = GetRepository();
        const string entityName = nameof(T);
        
        var existingEntity = await repository.GetAsync(entityId);

        if (existingEntity is null)
            return $"{entityName} not found";

        try
        {
            await repository.DeleteAsync(existingEntity);
            await GetUnitOfWork().CompleteAsync();

            return $"{entityName} successfully removed";
        }
        catch (Exception ex)
        {
            return $"An error occurred while deleting the {entityName}: {ex.Message}";
        }
    }
}