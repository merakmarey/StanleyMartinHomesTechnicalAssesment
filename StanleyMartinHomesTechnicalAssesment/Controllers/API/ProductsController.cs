using StanleyMartinHomesTechnicalAssesment.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace StanleyMartinHomesTechnicalAssesment.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductsRepository _productsRepository { get; set; }
        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<IActionResult> products(string query)
        {
            try
            {
                // Method A : 
                // var result = _productsRepository.Search_SegregatedData(query);


                // Method B
                var matches = _productsRepository.Search_Flattened_RelatedData(query);
                
                return Ok(new { matches });
            }
            catch (Exception)
            {
               return BadRequest(HttpStatusCode.InternalServerError);
                throw;
            }
        }
    }
}
