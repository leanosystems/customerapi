using AutoMapper;
using CustomerApi.Data;
using CustomerApi.Helpers;
using CustomerApi.Models;
using CustomerApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private DataContext dbContext;
        private IMapper mapper;
        public AccountController(DataContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AccountModel accountModel)
        {
            try
            {
                AccountValidator validator = new AccountValidator();
                var result = validator.Validate(accountModel);
                if (result.IsValid)
                {
                    var customer = await dbContext.Customer.FirstOrDefaultAsync(o => o.Id == accountModel.CustomerId);
                    if (customer != null)
                    {
                        await dbContext.Account.AddAsync(accountModel);
                        await dbContext.SaveChangesAsync();
                        return Ok("Added success.");
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

        [HttpGet("get-all-accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                var customers = await dbContext.Customer.ToListAsync();
                if (customers != null)
                {
                    var customersAccountModel = mapper.Map<List<CustomerAccountsDTO>>(customers);
                    foreach(var customer in customersAccountModel)
                    {
                        customer.FullName = GlobalFunctions.GenerateFullName(customer.FirstName, customer.LastName, customer.MiddleName);
                        var accounts = await dbContext.Account.Where(o => o.CustomerId == customer.Id).ToListAsync();
                        if (accounts.Any())
                        {
                            customer.Accounts.AddRange(accounts);
                        }
                    }
                    return Ok(customersAccountModel);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-customer-accounts/{customerId}")]
        public async Task<IActionResult> GetAccounts(int customerId)
        {
            try
            {
                var customer = await dbContext.Customer.FirstOrDefaultAsync(o => o.Id == customerId);
                if(customer != null)
                {
                    var customerAccountModel = mapper.Map<CustomerAccountsDTO>(customer);
                    customer.FullName = GlobalFunctions.GenerateFullName(customer.FirstName, customer.LastName, customer.MiddleName);
                    var accounts = await dbContext.Account.Where(o => o.CustomerId == customerId).ToListAsync();
                    if (accounts.Any())
                    {
                        customerAccountModel.Accounts.AddRange(accounts);
                    }
                    return Ok(customerAccountModel);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-account/{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            try
            {
                var accounts = await dbContext.Account.Where(o => o.Id == id).ToListAsync();
                if (accounts != null)
                    return Ok(accounts);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(AccountDTO accountDTO)
        {
            try
            {
                var currentAccount = await dbContext.Account.FirstOrDefaultAsync(o => o.Id == accountDTO.Id);
                if (currentAccount != null)
                {
                    var accountModel = mapper.Map<AccountModel>(accountDTO);
                    accountModel.InitialDeposit = currentAccount.InitialDeposit;
                    AccountValidator validator = new AccountValidator();
                    var result = validator.Validate(accountModel);
                    if (result.IsValid)
                    {
                        dbContext.Account.Entry(currentAccount).CurrentValues.SetValues(accountModel);
                        await dbContext.SaveChangesAsync();
                        return Ok("Updated success.");
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
                else
                    return NotFound();
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
                var currentAccount = await dbContext.Account.FirstOrDefaultAsync(o => o.Id == id);
                if (currentAccount != null)
                {
                    dbContext.Account.Remove(currentAccount);
                    await dbContext.SaveChangesAsync();
                    return Ok("Deleted success.");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
