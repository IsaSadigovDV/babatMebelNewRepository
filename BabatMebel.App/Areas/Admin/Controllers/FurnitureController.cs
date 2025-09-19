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
                Image = fd.Image,
                ImageUrl = fd.ImageUrl,
            }).ToListAsync();

            return View(furnitures);
        }


        public IActionResult Create() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FurnitureCreateDto dto)
        {
            if (dto.Image == null || !FileHelpers.IsImage(dto.Image))
            {
                return View(dto);
            }

            if (!FileHelpers.IsSizeValid(dto.Image, 20))
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
                Image = path,
                ImageUrl = fullPathImage,
            };

            await _furnitureWrite.AddAsync(furniture);
            await _furnitureWrite.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var furniture = await _furnitureRead.FilterFirstAsync(f => f.Id == id && !f.IsDeleted);

            if (furniture == null)
            {
                return NotFound();
            }

            var updateDto = new FurnitureUpdateDto()
            {
                Name = furniture.Name,
                Price = furniture.Price,
                Image = furniture.Image,
                ImageUrl = furniture.ImageUrl,
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, FurnitureUpdateDto dto)
        {
            var furniture = await _furnitureRead.FilterFirstAsync(f => f.Id == id && !f.IsDeleted);

            if (furniture == null)
            {
                return NotFound();
            }

            furniture.Price = dto.Price ?? furniture.Price;
            furniture.Name = dto.Name ?? furniture.Name;

            // image null deyilse image- yoxlamaq ve evvelki image-i silmek ve onun yerine upload etmek lazimdir.

            if (dto.ImageUpload != null)
            {
                if (!FileHelpers.IsImage(dto.ImageUpload) || !FileHelpers.IsSizeValid(dto.ImageUpload, 10))
                {
                    return BadRequest();
                }

                FileExtensions.DeleteFile(_env.WebRootPath, "uploads/furnitures", furniture.ImageUrl);


                var path = await dto.ImageUpload.SaveFileAsync(_env.WebRootPath, "uploads/furnitures");
                var request = HttpContext.Request;
                var scheme = request.Scheme;
                var host = request.Host;
                var baseUrl = $"{scheme}://{host}";

                var fullPathImage = $"{baseUrl}/{path}";
                furniture.ImageUrl = fullPathImage;
                furniture.Image = path;
            }

            furniture.UpdatedAt = DateTime.Now;


            _furnitureWrite.Update(furniture);
            await _furnitureWrite.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var furniture = await _furnitureRead.FilterFirstAsync(f => f.Id == id && !f.IsDeleted);

            if (furniture == null)
            {
                return NotFound();
            }

            furniture.IsDeleted = true;

            if (furniture.ImageUrl != null)
            {
                FileExtensions.DeleteFile(_env.WebRootPath, "uploads/furnitures", furniture.ImageUrl);
            }

            await _furnitureWrite.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
