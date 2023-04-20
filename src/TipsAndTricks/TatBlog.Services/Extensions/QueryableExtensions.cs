using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Collections;
using TatBlog.Core.Contracts;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using TatBlog.Core.Entities;
using TatBlog.Core.DTO;

namespace TatBlog.Services.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        public static IQueryable<PostItem> ProjectToPostItem(this IQueryable<Post> query)
        {
            return query.Select(x => new PostItem
            {
                Id = x.Id,
                Title = x.Title,
                UrlSlug = x.UrlSlug,
                Meta = x.Meta,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                Published = x.Published,
                Category = x.Category,
                Tags = x.Tags,
                Author = x.Author
            });
        }
    }
}
