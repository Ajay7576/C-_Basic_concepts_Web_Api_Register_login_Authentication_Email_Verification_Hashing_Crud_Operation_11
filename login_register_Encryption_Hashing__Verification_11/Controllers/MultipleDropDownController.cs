using login_register_Encryption_Hashing__Verification_11.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace login_register_Encryption_Hashing__Verification_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultipleDropDownController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MultipleDropDownController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("Country")]
        public IActionResult GetCountry()
        {
            var Countries = _context.Countries.ToList();
            return Ok(Countries);
        }


        [HttpGet]
        [Route("State")]
        public IActionResult GetState(int countryId)
        {
            var states = _context.States.Where(x => x.CountryId == countryId).ToList();
            return Ok(states);
        }


        [HttpGet]
        [Route("City")]
        public IActionResult GetCity(int stateId)
        {
            var cities = _context.Cities.Where(x => x.StateId == stateId).ToList();
            return Ok(cities);
        }
    }
}
