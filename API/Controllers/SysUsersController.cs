using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBUtility;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysUsersController : ControllerBase
    {
        private readonly EFSqlContext _context;

        public SysUsersController(EFSqlContext context)
        {
            _context = new EFSqlContext();
        }

        // GET: api/SysUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SysUser>>> GetSysUser()
        {
          if (_context.SysUser == null)
          {
              return NotFound();
          }
            return await _context.SysUser.ToListAsync();
        }

        // GET: api/SysUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SysUser>> GetSysUser(string id)
        {
          if (_context.SysUser == null)
          {
              return NotFound();
          }
            var sysUser = await _context.SysUser.FindAsync(id);

            if (sysUser == null)
            {
                return NotFound();
            }

            return sysUser;
        }

        // PUT: api/SysUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSysUser(string id, SysUser sysUser)
        {
            if (id != sysUser.UserID)
            {
                return BadRequest();
            }

            _context.Entry(sysUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SysUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SysUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SysUser>> PostSysUser(SysUser sysUser)
        {
          if (_context.SysUser == null)
          {
              return Problem("Entity set 'EFPostgreSqlContext.SysUser'  is null.");
          }
            _context.SysUser.Add(sysUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SysUserExists(sysUser.UserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSysUser", new { id = sysUser.UserID }, sysUser);
        }

        // DELETE: api/SysUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSysUser(string id)
        {
            if (_context.SysUser == null)
            {
                return NotFound();
            }
            var sysUser = await _context.SysUser.FindAsync(id);
            if (sysUser == null)
            {
                return NotFound();
            }

            _context.SysUser.Remove(sysUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SysUserExists(string id)
        {
            return (_context.SysUser?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
