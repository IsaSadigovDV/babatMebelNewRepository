using BabatMebel.App.Context;
using BabatMebel.App.Dtos.EmployeeDtos;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.REmployee;
using BabatMebel.App.Repository.Abstracts.RPosition;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabatMebel.App.Areas.Admin.Controllers
{
    [Area("admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeWriteRepository _employeeWrite;
        private readonly IEmployeeReadRepository _employeeRead;
        private readonly IPositonReadRepository _positonRead;
        private readonly AppDbContext _context;
        public EmployeeController(IEmployeeWriteRepository employeeWrite, IEmployeeReadRepository employeeRead, IPositonReadRepository positonRead, AppDbContext context)
        {
            _employeeWrite = employeeWrite;
            _employeeRead = employeeRead;
            _positonRead = positonRead;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRead.FilterAll(e => !e.IsDeleted).Select(e => new EmployeeGetDto
            {
                Id = e.Id,
                Description = e.Description,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Positions = e.Positions.Select(p => p.Position.Name).ToList()
            }).ToListAsync();

            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _positonRead.FilterAll(p => !p.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            ViewBag.Positions = await _positonRead.FilterAll(p => !p.IsDeleted).ToListAsync();

            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                FirstName = dto.FirstName,
                Description = dto.Description,
                LastName = dto.LastName,
                Positions = dto.PositionIds.Select(p => new EmployeePosition
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    EmployeeId = Guid.NewGuid(),
                    PositionId = p
                }).ToList()
            };

            await _employeeWrite.AddAsync(employee);
            await _employeeWrite.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var employee = await _employeeRead.FilterFirstAsync(p => p.Id == id);

            if (employee == null) return NotFound();

            var dto = new EmployeeUpdateDto
            {
                Id = employee.Id,
                Description = employee.Description,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PositionIds = employee.Positions.Select(p => p.PositionId).ToList()
            };

            ViewBag.Positions = await _positonRead.FilterAll(p => !p.IsDeleted).ToListAsync();

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeUpdateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var employee = await _employeeRead.Table
                .Include(e => e.Positions)
                .ThenInclude(e => e.Position)
                .FirstOrDefaultAsync(p => p.Id == dto.Id);

            if (employee == null) return NotFound();

            ViewBag.Positions = await _positonRead.FilterAll(p => !p.IsDeleted).ToListAsync();

            employee.FirstName = dto.FirstName ?? employee.FirstName;
            employee.LastName = dto.LastName ?? employee.LastName;
            employee.Description = dto.Description ?? employee.Description;

            if (employee.Positions != null)
            {
                employee.Positions.Clear();

                foreach (var positionId in dto.PositionIds)
                {
                    employee.Positions.Add(new EmployeePosition
                    {
                        EmployeeId = employee.Id,
                        PositionId = positionId,
                        CreatedAt = DateTime.Now,
                        Employee = employee
                    });
                }
                _context.EmployeePositions.UpdateRange(employee.Positions);
            }

            _employeeWrite.Update(employee);
            await _employeeWrite.SaveAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
