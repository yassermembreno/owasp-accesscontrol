using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using owasp_acccesscontrol.Application.Interfaces;
using owasp_accesscontrol.Domain.Entities;

namespace owasp_accesscontrol.WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private ICustomerService customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            this.customerService = customerService;
        }
       
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            IEnumerable<Customer> customers = customerService.FindAll();
            if(customers == null)
            {
                _logger.LogError("Something went wrong.");
                return NotFound();
            }

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            return Ok(customerService.FindById(id));
        }

    }
}

