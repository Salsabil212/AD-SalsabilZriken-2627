namespace HelpdeskApi.Dtos
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}