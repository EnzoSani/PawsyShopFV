using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pawsy.Application.Dtos;
using Pawsy.Application.Services.Interface;
using PawsyShop.Api.Responses;
using System.Net;

namespace PawsyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService service, ILogger<ProductController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            var response = new APIResponse();
            try
            {
                var products = await _service.GetAllAsync();
                response.Result = products;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while fetching products.");
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            var response = new APIResponse();
            try
            {
                var product = await _service.GetByIdAsync(id);
                if (product == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Product not found.");
                    return NotFound(response);
                }

                response.Result = product;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"An error occurred while fetching product with ID {id}.");
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] ProductDto dto)
        {
            var response = new APIResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Invalid data.");
                    return BadRequest(response);
                }
                var created = await _service.CreateAsync(dto);
                response.Result = created;
                response.StatusCode = HttpStatusCode.Created;

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new product.");
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);

            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> Update(int id, [FromBody] ProductDto dto)
        {
            var response = new APIResponse();
            try
            {
                if (dto == null || id != dto.Id)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Invalid data.");
                    return BadRequest(response);
                }
                var updated = await _service.UpdateAsync(dto);
                if (!updated)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Product not found.");
                    return NotFound(response);
                }
                response.StatusCode = HttpStatusCode.NoContent;
                return StatusCode((int)HttpStatusCode.NoContent, response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating product with ID {id}.");
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);

            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            var response = new APIResponse();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Pet not found.");
                    return NotFound(response);
                }
                response.StatusCode = HttpStatusCode.NoContent;
                return StatusCode((int)HttpStatusCode.NoContent, response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting pet with ID {id}.");
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);

            }
        }
    }
}
