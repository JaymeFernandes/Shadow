using Domain.Models.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Mappings.Catalog
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            var categories = CategoriesDefault.Categories
                .Select((name, i) => new Category
                {
                    Id = i + 1,
                    Name = name
                })
                .ToList();

            builder.HasData(categories);

            builder.ToTable("Categories");
        }
    }
}


public static class CategoriesDefault
{
    public static readonly ICollection<string> Categories = new List<string>
        {
            "Action",
            "Adventure",
            "Comedy",
            "Drama",
            "Romance",
            "Fantasy",
            "Supernatural",
            "Horror",
            "Thriller",
            "Mystery",
            "Psychological",
            "Slice of Life",
            "Sci-Fi",
            "Mecha",
            "Military",
            "Historical",
            "Music",
            "Sports",
            "Martial Arts",
            "School Life",
            "Magic",
            "Crime",
            "Detective",
            "Dementia",
            "Game",
            "Survival",
            "Apocalypse",
            "Post-Apocalyptic",
            "Space",
            "Time Travel",
            "Isekai",
            "Reincarnation",
            "Parallel Worlds",
            "Demons",
            "Vampires",
            "Zombies",
            "Aliens",
            "Mythology",
            "Gods",
            "Cultivation",
            "Superpower",
            "Cyberpunk",
            "Steampunk",
            "Political",
            "Philosophy",
            "Tragedy",
            "Dark Fantasy",
            "High Fantasy",
            "Urban Fantasy",
            "Light Novel",
            "Shounen",
            "Shoujo",
            "Seinen",
            "Josei",
            "Harem",
            "Reverse Harem",
            "Ecchi",
            "Fanservice",
            "BL (Boys Love)",
            "GL (Girls Love)",
            "LGBTQIA+",
            "Drama Family",
            "Friendship",
            "Revenge",
            "Anti-Hero",
            "Healing",
            "Coming of Age",
            "Satire",
            "Parody",
            "Post-Truth",
            "Psychological Horror",
            "Psychological Thriller",
            "Noir",
            "Biography",
            "Documentary",
            "Experimental",
            "Abstract",
            "Virtual Reality",
            "Augmented Reality",
            "Artificial Intelligence",
            "Post-Human",
            "Meta",
            "Fourth Wall",
            "Breaking the Fourth Wall",
            "Memoir",
            "Fantasy Comedy",
            "Fantasy Romance",
            "Romantic Comedy",
            "Action Comedy",
            "Action Romance",
            "Supernatural Mystery"
        };
}
