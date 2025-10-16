using TodoListapp.Models.Domain;

namespace TodoListapp.Interfaces
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> CreateAsync(TodoItem TodoItem);

        Task<List<TodoItem>> GetAllAsync(Guid? userId = null, string? filterOn = null, string? filterQuery = null,
         string? sortBy = null, bool IsAscendng = true);

         Task<TodoItem?> GetbyIdAsync(Guid userId ,Guid id);


         Task<TodoItem?> UpdateAsync(Guid userId, Guid id, TodoItem TodoItem);

        Task<TodoItem?> DeleteAsync(Guid  id , Guid userId);

        

    }
}
