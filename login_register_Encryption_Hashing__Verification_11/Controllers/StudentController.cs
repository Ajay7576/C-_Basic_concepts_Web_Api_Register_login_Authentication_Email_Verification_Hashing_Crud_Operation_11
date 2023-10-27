using login_register_Encryption_Hashing__Verification_11.Data;
using login_register_Encryption_Hashing__Verification_11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static login_register_Encryption_Hashing__Verification_11.Models.Student;

namespace login_register_Encryption_Hashing__Verification_11.Controllers
{

    [Route("api/student")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task <IActionResult> GetAllStudents()
        {

           var studentlist = 
            (from s in _context.Students
             join cn in _context.Countries on s.CountryId equals cn.Id
             join st in _context.States on s.StateId equals st.Id
             join ct in _context.Cities on s.CityId equals ct.Id


             select new
             {
                 Id = s.ID,
                 Name = s.Name,
                 Address = s.Address,
                 Email = s.Email,
                 Subscribe=s.Subscribe,
                 ContectNo=s.ContectNo,
                 Age=s.Age,
                 Picture=s.Picture,
                 Gender=s.Gender,
                 CreatedOn=s.CreatedOn,
                 CountryId  = s.CountryId,
                 StateId = s.StateId,
                 CityId = s.CityId,
                 Country = cn.Name,
                 State = st.Name,
                 City  = ct.Name

             }).ToList();
            return Ok(studentlist);
        }


        [HttpPost]
        public async Task<IActionResult> SaveStudent([FromBody]  Student student)
        {
            if (student != null && ModelState.IsValid)
            {

                //try
                //{
                //    var files = HttpContext.Request.Form.Files;
                //    if (files.Count > 0)
                //    {
                //        byte[] p1 = null;
                //        using (var fs1 = files[0].OpenReadStream())
                //        {
                //            using (var ms1 = new MemoryStream())
                //            {
                //                fs1.CopyTo(ms1);
                //                p1 = ms1.ToArray();
                //            }
                //        }
                //        student.Picture = p1;
                //        await _context.Students.AddAsync(student);
                //        _context.SaveChanges();

                //        return Ok(student);
                //    }
                //    else
                //    {
                //        return BadRequest();
                //    }
                //}
                //catch
                //{

                //    return BadRequest();
                //}


                await _context.Students.AddAsync(student);
                     _context.SaveChanges();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {

            if (student != null && ModelState.IsValid)
            {
                try
                {

                    var checkStudentId = _context.Students.FirstOrDefaultAsync(i => i.ID == student.ID);

                    if (checkStudentId != null)
                    {
                        _context.Students.Update(student);
                        _context.SaveChanges();
                        return Ok();
                    }
                    else
                        return BadRequest();
                }

                catch
                {
                    return BadRequest();

                }

            }
            return BadRequest();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var studentIndb = await _context.Students.FindAsync(id);
                if (studentIndb == null) return NotFound();
                _context.Students.Remove(studentIndb);
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
