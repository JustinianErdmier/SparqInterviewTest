namespace ArticlesApi.Models;

/// <summary>Represents an article with a unique identifier, title, and text content.</summary>
public sealed class Article
{
    /// <summary>Gets or sets the unique identifier for the article. This property is used to uniquely identify an instance of the <see cref="Article" /> class.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the main text content of the article. This property contains the body or full text of the <see cref="Article" /> class.</summary>
    public string? Text { get; set; }

    /// <summary>
    ///     Gets or sets the title of the article. This property represents the name or headline of the <see cref="Article" /> instance and must not be empty or consist solely of
    ///     whitespace.
    /// </summary>
    public string Title { get; set; } = null!;
}
