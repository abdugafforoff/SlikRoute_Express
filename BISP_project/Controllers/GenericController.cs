using BIS_project.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BIS_project.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    [Authorize]
    public class GenericController<T, Tdto> : ControllerBase where T : class where Tdto : class
    {
        private readonly IGenericService<T, Tdto> _genericService;

        public GenericController(IGenericService<T, Tdto> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet("getAll")]
        public async Task<List<T>> GetAllAsync()
        {
            return await _genericService.GetAll();
        }

        [HttpPost("insert")]
        public async Task<bool> InsertAsync(Tdto tDto)
        {
            return await _genericService.Insert(tDto);
        }

        [HttpDelete("delete/{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _genericService.Delete(id);
        }

        [HttpGet("getById/{id}")]
        public async Task<T> GetByIdAsync(int id)
        {
            return await _genericService.GetById(id);
        }

        [HttpPut("update/{id}")]
        public async Task<T> Update(int id, Tdto dto)
        {
            return await _genericService.Update(id, dto);
        }

    }
}