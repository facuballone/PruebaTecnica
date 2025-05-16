using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
using PruebaTecnica.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Controllers
{
    [Route("api/machines/{machineId}/[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComponentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/machines/5/components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component>>> GetComponents(int machineId)
        {
            // Verificar si la máquina existe
            if (!MachineExists(machineId))
            {
                return NotFound("Machine not found");
            }

            return await _context.Component
                .Where(c => c.MachineId == machineId)
                .ToListAsync();
        }

        // GET: api/machines/5/components/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetComponent(int machineId, int id)
        {
            // Verificar si la máquina existe
            if (!MachineExists(machineId))
            {
                return NotFound("Machine not found");
            }

            var component = await _context.Component
                .FirstOrDefaultAsync(c => c.Id == id && c.MachineId == machineId);

            if (component == null)
            {
                return NotFound();
            }

            return component;
        }

        // POST: api/machines/5/components
        [HttpPost]
        public async Task<IActionResult> PostComponent(int machineId, Component component)
        {
            if (!MachineExists(machineId))
            {
                return NotFound("Machine not found");
            }

            component.MachineId = machineId;
            component.Machine = null; // Por si viene con un objeto Machine anidado en el JSON

            _context.Component.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComponent), new { machineId = machineId, id = component.Id }, component);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int machineId, int id)
        {
            var component = await _context.Component
                .FirstOrDefaultAsync(c => c.Id == id && c.MachineId == machineId);

            if (component == null)
            {
                return NotFound();
            }

            _context.Component.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MachineExists(int id)
        {
            return _context.Machine.Any(e => e.Id == id);
        }

    }
}