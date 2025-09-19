namespace BabatMebel.App.Dtos.FurnitureDtos
{
    public class FurnitureGetDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
    }
}
