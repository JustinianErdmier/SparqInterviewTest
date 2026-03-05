namespace ArticlesApi.Services;

/// <summary>Provides in-memory management of articles, allowing for creation, retrieval, updating, and deletion of articles. Implements the <see cref="IRepository" /> interface.</summary>
public sealed class ArticlesRepository : IRepository
{
    private static readonly List<Article> Articles =
    [
        new()
        {
            Id    = Guid.NewGuid(),
            Title = "Getting Started with C#",
            Text  = "C# is a powerful, modern, object-oriented language."
        },
        new()
        {
            Id    = Guid.NewGuid(),
            Title = "Mastering ASP.NET Core",
            Text  = "ASP.NET Core is a high-performance, cross-platform framework for building modern cloud-based apps."
        },
        new()
        {
            Id    = Guid.NewGuid(),
            Title = "Introduction to Microservices",
            Text  = "Microservices are an architectural style that structures an application as a collection of services."
        }
    ];

    /// <inheritdoc />
    public List<Article> Get() => Articles;

    /// <inheritdoc />
    public Article? Get(Guid id) => Articles.FirstOrDefault(a => a.Id == id);

    /// <inheritdoc />
    public Guid Create(Article article)
    {
        ThrowIfArticleOrTitleIsMissing(article);

        article.Id = Guid.NewGuid();

        Articles.Add(article);

        return article.Id;
    }

    /// <inheritdoc />
    public bool Delete(Guid id)
    {
        Article? article = Articles.FirstOrDefault(a => a.Id == id);

        if (article is null)
        {
            return false;
        }

        Articles.Remove(article);

        return true;
    }

    /// <inheritdoc />
    public bool Update(Article articleToUpdate)
    {
        ThrowIfArticleOrTitleIsMissing(articleToUpdate);

        Article? article = Articles.FirstOrDefault(a => a.Id == articleToUpdate.Id);

        if (article is null)
        {
            return false;
        }

        article.Text  = articleToUpdate.Text;
        article.Title = articleToUpdate.Title;

        return true;
    }

    private static void ThrowIfArticleOrTitleIsMissing(Article? article)
    {
        ArgumentNullException.ThrowIfNull(article);
        ArgumentNullException.ThrowIfNull(article.Title);

        if (string.IsNullOrWhiteSpace(article.Title))
        {
            throw new ArgumentException(message: "Article title cannot be empty or whitespace.", nameof(article.Title));
        }
    }
}
