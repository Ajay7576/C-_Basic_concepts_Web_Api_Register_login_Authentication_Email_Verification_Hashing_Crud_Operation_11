using login_register_Encryption_Hashing__Verification_11.Data;
using login_register_Encryption_Hashing__Verification_11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_register_Encryption_Hashing__Verification_11.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getAllPatient()
        {

            var patientlist = await _context.Patients.ToListAsync();
            return Ok(patientlist);
        }


        [HttpGet]
        [Route("Navbaar")]
        public async Task<IActionResult> getNavbaarContent()
        {

            var list = await _context.Navbaar.ToListAsync();

            return Ok(list);
        }


        [HttpPost]
        public async Task<IActionResult> SaveStudent([FromBody] Patient patient)
        {
            if (patient != null && ModelState.IsValid)
            {
                try
                {
                  await  _context.Patients.AddAsync(patient);
                    _context.SaveChanges();
                    return Ok(patient);
                }
            
                catch
                {

                    return BadRequest();
                }

            }
            return BadRequest();
        }

        [HttpPut]
        [Route("updatePatientDetails")]
        public async Task<IActionResult> UpdateStudent([FromBody] Patient patient)
        {

            if (patient != null && ModelState.IsValid)
            {
                try
                { 
                    _context.Patients.Update(patient);
                     _context.SaveChanges();
                  
                }

                catch
                {
                    return BadRequest();

                }

            }
            return Ok(patient);

        }



        [HttpGet]
        [Route("getPatient")]
        public async Task<IActionResult> GetPatientId(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return BadRequest();
            else
                return Ok(patient);
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var patientIndb = await _context.Patients.FindAsync(id);
                if (patientIndb == null) return NotFound();
                _context.Patients.Remove(patientIndb);
                _context.SaveChanges();
                return Ok();

            }
            catch
            {

                return BadRequest();
            }

        }
    }
}
