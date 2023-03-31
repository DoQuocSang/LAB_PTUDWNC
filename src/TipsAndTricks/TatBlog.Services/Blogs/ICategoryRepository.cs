using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs;

public interface ICategoryRepository
{
	Task<Category> GetCategoryBySlugAsync(
		string slug,
		CancellationToken cancellationToken = default);

	Task<Category> GetCachedCategoryBySlugAsync(
		string slug, CancellationToken cancellationToken = default);

	Task<Category> GetCategoryByIdAsync(int CategoryId);

	Task<Category> GetCachedCategoryByIdAsync(int CategoryId);

	Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(
		IPagingParams pagingParams,
		string name = null,
		CancellationToken cancellationToken = default);

	Task<IPagedList<T>> GetPagedCategoriesAsync<T>(
		Func<IQueryable<Category>, IQueryable<T>> mapper,
		IPagingParams pagingParams,
		string name = null,
		CancellationToken cancellationToken = default);

	Task<bool> AddOrUpdateAsync(
		Category Category,
		CancellationToken cancellationToken = default);

	Task<bool> DeleteCategoryAsync(
		int CategoryId,
		CancellationToken cancellationToken = default);

	Task<bool> IsCategorySlugExistedAsync(
		int CategoryId, string slug,
		CancellationToken cancellationToken = default);

	Task<Category> CreateOrUpdateCategoryAsync(
	  Category Category, CancellationToken cancellationToken = default);

	Task<IPagedList<Category>> GetPagedCategoriesAsync(
		int pageNumber = 1,
		int pageSize = 10,
		CancellationToken cancellationToken = default);

	Task<IList<CategoryItem>> GetCategoriesAsync(
		bool showOnMenu = false,
		CancellationToken cancellationToken = default);
}