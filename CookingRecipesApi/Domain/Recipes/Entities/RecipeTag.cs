namespace Domain.Recipes.Entities;

public class RecipeTag
{
    public int RecipeId { get; private set; }
    public int TagId { get; private set; }
    public Recipe Recipe { get; private set; }
    public Tag Tag { get; private set; }

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
