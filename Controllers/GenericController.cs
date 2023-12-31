using BIS_project.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BIS_project.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenericController<T, Tdto> : ControllerBase where T : class where Tdto : class
    {
        private readonly IGenericService<T, Tdto> _genericService;

        public GenericController(IGenericService<T, Tdto> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet("GetAll")]
        public async Task<List<T>> GetAllAsync()
        {
            return await _genericService.GetAll();
        }

        [HttpPost("Insert")]
        public async Task<bool> InsertAsync(Tdto tDto)
        {
            return await _genericService.Insert(tDto);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _genericService.Delete(id);
        }

        [HttpGet("GetById/{id}")]
        public async Task<T> GetByIdAsync(int id)
        {
            return await _genericService.GetById(id);
        }

        [HttpPut("Update/{id}")]
        public async Task<T> Update(int id, Tdto dto)
        {
            return await _genericService.Update(id, dto);
        }

    }
}