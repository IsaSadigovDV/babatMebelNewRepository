using BabatMebel.App.Dtos.ContactDtos;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RContact;
using Microsoft.AspNetCore.Mvc;

namespace BabatMebel.App.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactReadRepository _contactRead;
        private readonly IContactWriteRepository _contactWrite;
        public ContactController(IContactReadRepository contactRead, IContactWriteRepository contactWrite)
        {
            _contactRead = contactRead;
            _contactWrite = contactWrite;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                CreatedAt = DateTime.Now,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Message = dto.Message,
                Status = false,
            };

            await _contactWrite.AddAsync(contact);
            await _contactWrite.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
