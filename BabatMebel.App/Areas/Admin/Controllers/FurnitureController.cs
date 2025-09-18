using BabatMebel.App.Dtos.FurnitureDtos;
using BabatMebel.App.Entities;
using BabatMebel.App.Extensions;
using BabatMebel.App.Helpers;
using BabatMebel.App.Repository.Abstracts.RFurniture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabatMebel.App.Areas.Admin.Controllers
{
    [Area("admin")]
    public class FurnitureController : Controller
    {
        private readonly IFurnitureReadRepository _furnitureRead;
        private readonly IFurnitureWriteRepository _furnitureWrite;
        private readonly IWebHostEnvironment _env;

        public FurnitureController(IFurnitureReadRepository furnitureRead, IFurnitureWriteRepository furnitureWrite, IWebHostEnvironment env)
        {
            _furnitureRead = furnitureRead;
            _furnitureWrite = furnitureWrite;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var furnitures = await _furnitureRead.FilterAll(f => !f.IsDeleted).Select(fd => new FurnitureGetDto
            {
                Id = fd.Id,
                Name = fd.Name,
                Price = fd.Price,
                Created = fd.CreatedAt,
                ImageUrl = fd.ImageUrl,
            }).ToListAsync();

            return View(furnitures);
        }


        public IActionResult Create() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FurnitureCreateDto dto)
        {
            if (dto.Image == null || FileHelpers.IsImage(dto.Image))
            {
                return View(dto);
            }

            if (!FileHelpers.IsSizeValid(dto.Image, 5))
            {
                return View(dto);
            }

            var path = await dto.Image.SaveFileAsync(_env.WebRootPath, "uploads/furnitures");
            var request = HttpContext.Request;
            var scheme = request.Scheme;
            var host = request.Host;
            var baseUrl = $"{scheme}://{host}";

            var fullPathImage = $"{baseUrl}/{path}";

            // 
            var furniture = new Furniture()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                CreatedAt = DateTime.Now,
                ImageUrl = fullPathImage,
            };

            await _furnitureWrite.AddAsync(furniture);
            await _furnitureWrite.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
