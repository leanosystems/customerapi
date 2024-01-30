using CustomerApi.Data;
using CustomerApi.Helpers;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private DataContext dbContext;
        public CustomerController(DataContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CustomerModel customerModel)
        {
            try
            {
                CustomerValidator validator = new CustomerValidator();
                var result = validator.Validate(customerModel);
                if (result.IsValid)
                {
                    customerModel.Age = GlobalFunctions.GenerateAge(customerModel.DateOfBirth);
                    await dbContext.Customer.AddAsync(customerModel);
                    await dbContext.SaveChangesAsync();
                    return Ok("Added success.");
                }
                else
                    return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await dbContext.Customer.ToListAsync();
            foreach(var item in result)
            {
                item.FullName = GlobalFunctions.GenerateFullName(item.FirstName, item.LastName, item.MiddleName);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await dbContext.Customer.FirstOrDefaultAsync(o => o.Id == id);
                model.FullName = GlobalFunctions.GenerateFullName(model.FirstName, model.LastName, model.MiddleName);
                if (model != null)
                    return Ok(model);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(CustomerModel customerModel)
        {
            try
            {
                CustomerValidator validator = new CustomerValidator();
                var result = validator.Validate(customerModel);
                if (result.IsValid)
                {
                    var currentCustomer = await dbContext.Customer.FirstOrDefaultAsync(o => o.Id == customerModel.Id);
                    if (currentCustomer != null)
                    {
                        customerModel.Age = GlobalFunctions.GenerateAge(customerModel.DateOfBirth);
                        dbContext.Customer.Entry(currentCustomer).CurrentValues.SetValues(customerModel);
                        await dbContext.SaveChangesAsync();
                        return Ok("Updated success.");
                    }
                    else
                        return NotFound();
                }
                else
                    return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await dbContext.Customer.FirstOrDefaultAsync(o => o.Id == id);
                if (model != null)
                {
                    dbContext.Customer.Remove(model);
                    await dbContext.SaveChangesAsync();
                    return Ok("Deleted success.");
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
