using AspNetBeginner.Data;
using AspNetBeginner.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetBeginner.Controllers
{
    [ApiController]
    [Route("controller")]
    [Authorize]

    public class productsController:ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<productsController> _logger;

        public productsController(ApplicationDbContext dbContext,ILogger<productsController>logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Product>> Get() 
        {
            var userName = User.Identity.Name;
            
            
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var record = _dbContext.Set<Product>().ToList();
            return Ok(record);
        }
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        [LogSenstivity]

        public ActionResult<Product> GetbyId(int id) 
        {
            _logger.LogDebug("Getting product # "+ id);

            //_logger.logdebug("getting product #{ id}", id);
            var record = _dbContext.Set<Product>().Find(id);
            if (record == null)
                _logger.LogWarning("Product #{x} was not found--- time{y}", id,DateTime.Now); 
            return record== null?NotFound(): Ok(record);


        }
        [HttpPost]
        
        [Route("")]
        [AllowAnonymous]
        public ActionResult<int> Createproduct(Product product)
        {
            product.Id = 0;
            _dbContext.Set<Product>().Add(product);
            _dbContext.SaveChanges();
            return Ok(product.Id);
        }
        [HttpPut]
        [Route("")]
        public ActionResult UpdateProduct(Product product)
        {
            var exisistingproduct = _dbContext.Set<Product>().Find(product.Id);
            exisistingproduct.Name = product.Name;
            exisistingproduct.Sku = product.Sku;
            _dbContext.Set<Product>().Update(exisistingproduct);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(int id) 
        {
            var exisistingproduct = _dbContext.Set<Product>().Find(id);
            _dbContext.Set<Product>().Remove(exisistingproduct);
            _dbContext.SaveChanges();
            return Ok();
        }

    }
}
