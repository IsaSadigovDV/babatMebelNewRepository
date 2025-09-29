using BabatMebel.App.Dtos.BasketDtos;
using BabatMebel.App.Dtos.FurnitureDtos;
using BabatMebel.App.Repository.Abstracts.RBasket;
using BabatMebel.App.Repository.Abstracts.RFurniture;
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

        public ShopController(IFurnitureReadRepository furnitureRead, IBasketReadRepository basketRead, IBasketWriteRepository basketWrite)
        {
            _furnitureRead = furnitureRead;
            _basketRead = basketRead;
            _basketWrite = basketWrite;
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
            //
            var cookiJson = Request.Cookies["basket"];

            if (cookiJson == null)
            {
                List<BasketDto> baskets = new List<BasketDto>();

                BasketDto basketDto = new BasketDto()
                {
                    Count = 1,
                    FurnitureId = id
                };
                baskets.Add(basketDto);
                cookiJson = JsonConvert.SerializeObject(baskets);

                Response.Cookies.Append("basket", cookiJson);
            }
            else
            {
                List<BasketDto> baskets = JsonConvert.DeserializeObject<List<BasketDto>>(cookiJson);

                BasketDto basket = baskets.FirstOrDefault(f => f.FurnitureId == id);

                if (basket != null)
                {
                    basket.Count++;
                }
                else
                {
                    BasketDto basketDto = new BasketDto();
                    basketDto.Count = 1;
                    basketDto.FurnitureId = id;
                    baskets.Add(basketDto);
                }
                cookiJson = JsonConvert.SerializeObject(baskets);
                Response.Cookies.Append("basket", cookiJson);
            }

            TempData["basket"] = "salam";

            return RedirectToAction(nameof(Index));

        }
    }
}
