namespace ArticlesApi.Services;

/// <summary>Defines methods for managing and accessing articles in a repository.</summary>
public interface IRepository
{
    /// <summary>Retrieves the complete list of articles from the repository.</summary>
    /// <returns>A list of <see cref="Article" /> objects representing all articles in the repository.</returns>
    List<Article> Get();

    /// <summary>Retrieves an article by its unique identifier.</summary>
    /// <param name="id">The unique identifier of the article to retrieve.</param>
    /// <returns>Returns the article with the specified identifier if it exists; otherwise, <c>null</c>.</returns>
    Article? Get(Guid id);

    /// <summary>Adds a new article to the repository.</summary>
    /// <param name="article">The article to add to the repository.</param>
    /// <returns>Returns the unique identifier of the newly created article.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the provided article or title is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the provided article's title is empty or only whitespace.</exception>
    Guid Create(Article article);

    /// <summary>Deletes an article with the specified identifier.</summary>
    /// <param name="id">The unique identifier of the article to delete.</param>
    /// <returns>Returns <c>true</c> if the article was successfully deleted; otherwise, <c>false</c> if no article with the specified identifier was found.</returns>
    bool Delete(Guid id);

    /// <summary>Updates an existing article with the provided information.</summary>
    /// <param name="articleToUpdate">The article containing the updated information. The article must have a valid identifier.</param>
    /// <returns>Returns <c>true</c> if the article was successfully updated; otherwise, <c>false</c> if no article with the specified identifier was found.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the provided article or title is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the provided article's title is empty or only whitespace.</exception>
    bool Update(Article articleToUpdate);
}
