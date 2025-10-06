using BabatMebel.App.Dtos.BasketDtos;
using BabatMebel.App.Dtos.FurnitureDtos;
using BabatMebel.App.Repository.Abstracts.RBasket;
using BabatMebel.App.Repository.Abstracts.RFurniture;
using BabatMebel.App.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BabatMebel.App.Controllers
{
    public class ShopController : Controller
    {
        private readonly IFurnitureReadRepository _furnitureRead;
        private readonly IBasketReadRepository _basketRead;
        private readonly IBasketWriteRepository _basketWrite;
        private readonly IBasketService _basketService;

        public ShopController(IFurnitureReadRepository furnitureRead, IBasketReadRepository basketRead, IBasketWriteRepository basketWrite, IBasketService basketService)
        {
            _furnitureRead = furnitureRead;
            _basketRead = basketRead;
            _basketWrite = basketWrite;
            _basketService = basketService;
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

        public async Task<IActionResult> AddBasket(Guid id)
        {

            // loginsizik
            var res = await _basketService.AddBasket(id);

            TempData["basket"] = "salam";

            return RedirectToAction(nameof(Index));
        }

        public async Task<List<BasketItemDto>> GetAllBasket()
        {
            var jsonBasket = Request.Cookies["basket"];

            if (jsonBasket != null)
            {
                List<BasketDto> basketDtos = JsonConvert.DeserializeObject<List<BasketDto>>(jsonBasket);

                List<BasketItemDto> basketItemDtos = new();


                foreach (var item in basketDtos)
                {
                    var furniture = await _furnitureRead.FilterFirstAsync(f => !f.IsDeleted && f.Id == item.FurnitureId);

                    if (furniture != null)
                    {
                        basketItemDtos.Add(new BasketItemDto
                        {
                            FurnitureId = item.FurnitureId,
                            Count = item.Count,
                            ImageUrl = furniture.ImageUrl,
                            Name = furniture.Name,
                            Price = furniture.Price,
                        });
                    }
                }
                return basketItemDtos;
            }

            return new List<BasketItemDto>();
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            await _basketService.Remove(id);
            return RedirectToAction("index", "order");
        }
    }
}
