using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using TesteDotnet.Data;

namespace TesteDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly Context _context;
        public readonly IRepository _repo;

        public EntriesController(Context context, IRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/Entries
        [HttpGet]
        public ActionResult<IEnumerable<Entry>> GetEntry()
        {
            return _repo.GetAllEntries();
        }

        // GET: api/Entries/5
        [HttpGet("{id}")]
        public ActionResult<Entry> GetEntry(int id)
        {
            var entry = _repo.GetEntryById(id);

            if (entry == null)
            {
                return NotFound();
            }

            return entry;
        }

        // PUT: api/Entries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutEntry(int id, Entry entry)
        {
            if (id != entry.Id)
            {
                return BadRequest();
            }

            _repo.Update(entry);

            try
            {
                if (_repo.SaveChanges())
                {
                    return Ok(entry);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return BadRequest("Entry not updated");
        }

        // POST: api/Entries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Entry> PostEntry(Entry entry)
        {
            _repo.Add(entry);
            if (_repo.SaveChanges())
            {
                return Ok(entry);
            }

            return BadRequest("Entry not added");
        }

        // DELETE: api/Entries/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var entry = _repo.GetEntryById(id);
            if (entry == null)
            {
                return NotFound();
            }

            _repo.Delete(entry);
            if (_repo.SaveChanges())
            {
                return Ok(entry);
            }

            return BadRequest("Entry not deleted");
        }

        private bool EntryExists(int id)
        {
            if (_repo.GetEntryById(id) != null)
            {
                return true;
            }
            return false;
        }
    }
}
