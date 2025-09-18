using BabatMebel.App.Dtos.ContactDtos;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RContact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabatMebel.App.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ContactController : Controller
    {
        private readonly IContactReadRepository _contactRead;
        private readonly IContactWriteRepository _contactWrite;

        public ContactController(IContactReadRepository contactRead, IContactWriteRepository contactWrite)
        {
            _contactRead = contactRead;
            _contactWrite = contactWrite;
        }

        public async Task<IActionResult> Index()
        {
            var dto = await _contactRead.FilterAll(c => !c.IsDeleted).Select(c => new ContactGetDto
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Message = c.Message,
                Status = c.Status
            }).ToListAsync();

            return View(dto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            Contact contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                CreatedAt = DateTime.Now,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Message = dto.Message
            };

            //await _context.Contacts.AddAsync(contact);
            //await _context.SaveChangesAsync();

            await _contactWrite.AddAsync(contact);
            await _contactWrite.SaveAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _contactRead.FilterFirstAsync(c => c.Id == id && !c.IsDeleted);

            if (contact == null)
            {
                return NotFound();
            }

            var dto = new ContactGetDto()
            {
                Id = contact.Id,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Message = contact.Message,
                Status = contact.Status,
                CreatedAt = contact.CreatedAt,
            };
            return View(dto);
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var contact = await _contactRead.FilterFirstAsync(c => c.Id == id && !c.IsDeleted);

            if (contact == null)
            {
                return NotFound();
            }

            //contact.IsDeleted = true;
            //await _context.SaveChangesAsync();

            _contactWrite.SoftRemove(contact);
            await _contactWrite.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var contact = await _contactRead.FilterFirstAsync(c => c.Id == id && !c.IsDeleted);

            if (contact == null)
            {
                return NotFound();
            }

            contact.Status = true;
            await _contactWrite.SaveAsync();


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ReverseStatus(Guid id)
        {
            var contact = await _contactRead.FilterFirstAsync(c => c.Id == id && !c.IsDeleted);

            if (contact == null)
            {
                return NotFound();
            }

            contact.Status = false;
            await _contactWrite.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
