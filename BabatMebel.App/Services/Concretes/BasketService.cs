using BabatMebel.App.Dtos.BasketDtos;
using BabatMebel.App.Repository.Abstracts.RFurniture;
using BabatMebel.App.Services.Abstractions;
using Newtonsoft.Json;

namespace BabatMebel.App.Services.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IFurnitureReadRepository _furnitureRead;

        public BasketService(IHttpContextAccessor httpContext, IFurnitureReadRepository furnitureRead)
        {
            _httpContext = httpContext;
            _furnitureRead = furnitureRead;
        }

        public async Task<string> AddBasket(Guid id)
        {
            var cookiJson = _httpContext.HttpContext.Request.Cookies["basket"];

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

                _httpContext.HttpContext.Response.Cookies.Append("basket", cookiJson);
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
                _httpContext.HttpContext.Response.Cookies.Append("basket", cookiJson);
            }


            return "Basket added";
        }

        public async Task<List<BasketItemDto>> GetAllBasket()
        {
            var jsonBasket = _httpContext.HttpContext.Request.Cookies["basket"];

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

        public async Task Remove(Guid id)
        {
            var jsonBasket = _httpContext.HttpContext.Request.Cookies["basket"];
            if (jsonBasket != null)
            {
                List<BasketDto> basketDtos = JsonConvert.DeserializeObject<List<BasketDto>>(jsonBasket);

                var basket = basketDtos.FirstOrDefault(b => b.FurnitureId == id);

                if (basket != null)
                {
                    basketDtos.Remove(basket);
                    jsonBasket = JsonConvert.SerializeObject(basketDtos);
                    _httpContext.HttpContext.Response.Cookies.Append("basket", jsonBasket);
                }
            }

        }
    }
}
