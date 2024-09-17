namespace Domain.Recipes.Entities;

public class RecipeTag
{
    public int RecipeId { get; }
    public int TagId { get; }
    public Recipe Recipe { get; }
    public Tag Tag { get; }

    public RecipeTag() { }

    public RecipeTag( Tag tag )
    {
        Tag = tag;
    }

    public RecipeTag( int tagId, Tag tag )
    {
        TagId = tagId;
        Tag = tag;
    }
}
