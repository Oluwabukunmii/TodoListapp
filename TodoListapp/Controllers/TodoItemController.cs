using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListapp.CustomActionFilters;
using TodoListapp.Data;
using TodoListapp.Models.Domain;
using TodoListapp.Models.Dtos;
using TodoListapp.Repositories;

namespace TodoListapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TodoItemController : ControllerBase
    {
        private readonly Todolistdbcontext dbContext;
        private readonly IMapper mapper;

        public ITodoItemRepository TodoItemRepository { get; }

        public TodoItemController(Todolistdbcontext dbContext, IMapper mapper, ITodoItemRepository TodoItemRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.TodoItemRepository = TodoItemRepository;
        }





        //CREATE TodoItem
        //POST : api /TodoItem

        [HttpPost]
        [Authorize]



        public async Task<IActionResult> Create([FromBody] CreateTodoItemDto CreateTodoItemDto)

        {

            // Get the logged-in user's ID

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));             // converts string (userid extracted fro jwt)to guid before saving back to the database



            /*User → Represents the currently logged-in user (provided by ASP.NET Core via the JWT).

FindFirstValue(ClaimTypes.NameIdentifier) → Extracts the unique user ID from the JWT token. 
            This is crucial because we want to tie this Todo to this specific user.*/
            //Map Dto to Domain model

            var TodoItemDomainmodel = mapper.Map<TodoItem>(CreateTodoItemDto);

            // Asssign the logged-in user's ID to the Domainmodeluserid

            TodoItemDomainmodel.UserId = userId;

            /*Here’s the key part: we assign the logged-in user’s ID to the Todo item.

This ensures that this Todo item is owned by the current user. 
            Without this, all Todos would be “anonymous” and you couldn’t filter by user later.*/


            //Save changes to Database

            await TodoItemRepository.CreateAsync(TodoItemDomainmodel);

            //Map domain Model back to dto

            return Ok(mapper.Map<TodoItemDto>(TodoItemDomainmodel));


        }


        //GET ALL TODOITEMS

        //GET :http// localhost:portnumber/ api/TodoItem

        [HttpGet]

        public async Task<IActionResult> GetAll([FromQuery] string? FilterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? IsAscending)

        {
            // Get logged-in user ID from the JWT token

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))

            {
                return Unauthorized("User Todos not found");

            }

            var userId = Guid.Parse(userIdString);

            // GetAll data from database 

            var TodoItemDomainmodel = await TodoItemRepository.GetAllAsync(userId, FilterOn, filterQuery, sortBy, IsAscending ?? true);

            // Map domain model back to Dto

            var ListofTodoItem = mapper.Map<List<TodoItemDto>>(TodoItemDomainmodel);

            return Ok(ListofTodoItem);



        }

        
        //GET TODO ITEM

        //GET :http// localhost:portnumber/ api/TodoItem


         [HttpGet]
        [Route("{id:Guid}")]
        [Authorize]
        [ValidateModel]



        public async Task<IActionResult> GetbyId([FromRoute] Guid id)

        {

            // Get logged-in user ID from the JWT token

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))

            {
                return Unauthorized("User todos not found");

            }

            var userId = Guid.Parse(userIdString);

            //Get data from database

            var TodoItem = await TodoItemRepository.GetbyIdAsync(userId, id);

            if (TodoItem == null)
            {
                return NotFound();
            }

            //  Map domain model to Dto

            return Ok(mapper.Map<TodoItemDto>(TodoItem));

        }



        //UPDATE TO DO LIST

        // PUT:https://localhost:portnumber/api/UserList/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]


        public async Task<IActionResult> Update([FromRoute] Guid id ,[FromBody] updateTodoDto updateTodoDto)

        {

            // Get logged-in user ID from the JWT token

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))

            {
                return Unauthorized("User todos not found");

            }

            var userId = Guid.Parse(userIdString);


            //Map Dto to domain model

            var TodoDomainModel = mapper.Map<TodoItem>(updateTodoDto);

            //Save changes to Database


            var todoItem = await TodoItemRepository.UpdateAsync(userId, id, TodoDomainModel);



            if (todoItem == null)
            {
                return NotFound();
            }

            //Map updated domain Model back to dto

            return Ok(mapper.Map<TodoItemDto>(todoItem));



        }

        //DELETE TO DO LIST

        //DELETE: https://localhost:portnumber/api/UserList/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id )
        {

            // Get logged-in user ID from the JWT token

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))

            {
                return Unauthorized("User todos not found");

            }

            var userId = Guid.Parse(userIdString);

            //save chaanges to db

            var DeletedTodo= await TodoItemRepository.DeleteAsync(id, userId);


            if (DeletedTodo == null)

            {
                return NotFound();
            }
            // Delete region


            //Map domain model to Dto

            return Ok(mapper.Map<TodoItemDto>(DeletedTodo));


        }



    }
}