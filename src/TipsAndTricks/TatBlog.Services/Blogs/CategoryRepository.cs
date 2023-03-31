using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;

namespace TatBlog.Services.Blogs
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public CategoryRepository(BlogDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<Category> GetCategoryBySlugAsync(
            string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Category>()
                .FirstOrDefaultAsync(a => a.UrlSlug == slug, cancellationToken);
        }

        public async Task<Category> GetCachedCategoryBySlugAsync(
            string slug, CancellationToken cancellationToken = default)
        {
            return await _memoryCache.GetOrCreateAsync(
                $"category.by-slug.{slug}",
                async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                    return await GetCategoryBySlugAsync(slug, cancellationToken);
                });
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Set<Category>().FindAsync(categoryId);
        }

        public async Task<Category> GetCachedCategoryByIdAsync(int categoryId)
        {
            return await _memoryCache.GetOrCreateAsync(
                $"category.by-id.{categoryId}",
                async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                    return await GetCategoryByIdAsync(categoryId);
                });
        }

        public async Task<IList<CategoryItem>> GetCategoriessAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Category>()
                .OrderBy(a => a.Name)
                .Select(x => new CategoryItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    ShowOnMenu = x.ShowOnMenu,
                    PostCount = x.Posts.Count(p => p.Published)
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(
            IPagingParams pagingParams,
            string name = null,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Category>()
                .AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(name),
                    x => x.Name.Contains(name))
                .Select(x => new CategoryItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    ShowOnMenu = x.ShowOnMenu,
                    PostCount = x.Posts.Count(p => p.Published)
                })
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<T>> GetPagedCategoriesAsync<T>(
            Func<IQueryable<Category>, IQueryable<T>> mapper,
            IPagingParams pagingParams,
            string name = null,
            CancellationToken cancellationToken = default)
        {
            var categoryQuery = _context.Set<Category>().AsNoTracking();

            if (!string.IsNullOrEmpty(name))
            {
                categoryQuery = categoryQuery.Where(x => x.Name.Contains(name));
            }

            return await mapper(categoryQuery)
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<bool> AddOrUpdateAsync(
            Category category, CancellationToken cancellationToken = default)
        {
            if (category.Id > 0)
            {
                _context.Categories.Update(category);
                _memoryCache.Remove($"category.by-id.{category.Id}");
            }
            else
            {
                _context.Categories.Add(category);
            }

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteCategoryAsync(
            int categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .Where(x => x.Id == categoryId)
                .ExecuteDeleteAsync(cancellationToken) > 0;
        }

        public async Task<bool> IsCategorySlugExistedAsync(
            int categoryId,
            string slug,
            CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AnyAsync(x => x.Id != categoryId && x.UrlSlug == slug, cancellationToken);
        }

        public async Task<Category> CreateOrUpdateCategoryAsync(
          Category category, CancellationToken cancellationToken = default)
        {
            if (category.Id > 0)
            {
                _context.Set<Category>().Update(category);
            }
            else
            {
                _context.Set<Category>().Add(category);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return category;
        }

        public async Task<IPagedList<Category>> GetPagedCategoriesAsync(
         int pageNumber = 1,
         int pageSize = 10,
         CancellationToken cancellationToken = default)
        {
            //var pagingParams = new PagingParams
            //{
            //    PageNumber = 1, //Số thứ tự của trang
            //    PageSize = 6, //Số mẫu tin trong 1 trang
            //    SortColumn = "Name",
            //    SortOrder = "ASC"
            //};
            var categoryQuery = _context.Set<Category>()
                .Include(x => x.Posts);

            return await categoryQuery.ToPagedListAsync(
                pageNumber, pageSize,
                nameof(Category.Name), "ASC",
                cancellationToken);
        }

        public async Task<IList<CategoryItem>> GetCategoriesAsync(
            bool showOnMenu = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Category> categories = _context.Set<Category>();

            if (showOnMenu)
            {
                categories = categories.Where(x => x.ShowOnMenu);
            }

            return await categories
                .OrderBy(x => x.Name)
                .Select(x => new CategoryItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    ShowOnMenu = x.ShowOnMenu,
                    PostCount = x.Posts.Count(p => p.Published)
                })
                .ToListAsync(cancellationToken);
        }
    }
}
