using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TodoListapp.Data;
using TodoListapp.Models.Domain;

namespace TodoListapp.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly Todolistdbcontext dbContext;

        public TodoItemRepository(Todolistdbcontext dbContext)
        {
            this.dbContext = dbContext;
        }



        public async Task<TodoItem> CreateAsync(TodoItem TodoItem)
        {
            await dbContext.TodoItems.AddAsync(TodoItem);
            await dbContext.SaveChangesAsync();

            return TodoItem;
        }

      

        public async Task<List<TodoItem>> GetAllAsync(Guid? userId = null, string? filterOn = null, string? filterQuery = null,
         string? sortBy = null, bool IsAscending = true)

        {




            var todoItem = dbContext.TodoItems.Include(x => x.User).AsQueryable();



            //Restrict to current user
            if (userId.HasValue) //checking nullable guid
            {
                todoItem = todoItem.Where(x => x.UserId == userId.Value);
            }

            //Filtering

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))

            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))

                {

                    todoItem = todoItem.Where(x => x.Title.Contains(filterQuery));

                }
                else

                if (filterOn.Equals("Status", StringComparison.OrdinalIgnoreCase))

                {

                    todoItem = todoItem.Where(x => x.Title.Contains(filterQuery));
                }

                else

                 if (filterOn.Equals("CreatedDate", StringComparison.OrdinalIgnoreCase))
                {
                    // Try to parse date

                    if (DateTime.TryParse(filterQuery, out var date))
                    {
                        // Compare only date part, ignoring time

                        todoItem = todoItem.Where(x => x.CreatedDate.Date == date.Date);


                    }

                }
            }

            //Sorting

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    todoItem = IsAscending ? todoItem.OrderBy(x => x.Title) : todoItem.OrderByDescending(x => x.Title);
                }
                else if (sortBy.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
                {
                    todoItem = IsAscending ? todoItem.OrderBy(x => x.CreatedDate) : todoItem.OrderByDescending(x => x.CreatedDate);
                }
                else if (sortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    todoItem = IsAscending ? todoItem.OrderBy(x => x.Status) : todoItem.OrderByDescending(x => x.Status);
                }
            }




            return await todoItem.ToListAsync();


        }





        public Task<TodoItem?> GetbyIdAsync(Guid userId, Guid id)
        {
            var todoItem = dbContext.TodoItems.Include(x => x.User)
                                               .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);


            return todoItem;


        }


        
        public async Task<TodoItem?> UpdateAsync(Guid userId, Guid id, TodoItem TodoItem)
        {

            var existingTodo = await dbContext.TodoItems.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (existingTodo == null)

            { return null; }

            existingTodo.Title = TodoItem.Title;
            existingTodo.Description = TodoItem.Description;
            existingTodo.Status = TodoItem.Status;

            await dbContext.SaveChangesAsync();

            return existingTodo;

        }


        public async Task<TodoItem?> DeleteAsync(Guid id, Guid userId)
        {
            var existingTodo = await dbContext.TodoItems.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (existingTodo == null)

            { return null; }

           

            dbContext.TodoItems.Remove(existingTodo);


            await dbContext.SaveChangesAsync();

            return existingTodo;

        }

    }
}
 
