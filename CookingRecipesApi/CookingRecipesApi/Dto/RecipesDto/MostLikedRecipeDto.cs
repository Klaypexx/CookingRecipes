namespace CookingRecipesApi.Dto.RecipesDto;

public class MostLikedRecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public int CookingTime { get; set; }
    public string AvatarPath { get; set; }
    public int LikeCount { get; set; }
}
