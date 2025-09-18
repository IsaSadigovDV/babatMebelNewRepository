namespace BabatMebel.App.Dtos.FurnitureDtos
{
    public class FurnitureCreateDto
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
