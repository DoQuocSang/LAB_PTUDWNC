using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using SlugGenerator;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TatBlog.Core.Collections;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApi.Extensions;
using TatBlog.WebApi.Filters;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Endpoints
{
    public static class PostEndpoints
    {
        public static WebApplication MapPostEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/posts");

            routeGroupBuilder.MapGet("/", GetPosts)
                .WithName("GetPosts")
                .Produces<ApiResponse<PaginationResult<PostItem>>>();

            routeGroupBuilder.MapGet("/featured/{limit}", GetFeaturedPosts)
                .WithName("GetFeaturedPosts")
                .Produces<ApiResponse<PaginationResult<PostItem>>>();

            routeGroupBuilder.MapGet("/random/{limit}", GetRandomPosts)
               .WithName("GetRandomPosts")
               .Produces<ApiResponse<PaginationResult<PostItem>>>();

            //routeGroupBuilder.MapGet("/archives/{limit}", GetPosts)
            //   .WithName("GetPosts")
            //   .Produces<ApiResponse<PaginationResult<PostItem>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetPostDetails)
                .WithName("GetPostById")
                .Produces<ApiResponse<PostItem>>();

            routeGroupBuilder.MapGet(
                    "/byslug/{slug:regex(^[a-z0-9_-]+$)}",
                    GetPostsBySlug)
                .WithName("GetPostsBySlug")
                .Produces<ApiResponse<PaginationResult<PostDto>>>();

            //routeGroupBuilder.MapPost("/", AddPost)
            //    .WithName("AddNewPost")
            //    .AddEndpointFilter<ValidatorFilter<PostEditModel>>()
            //    .RequireAuthorization()
            //    .Produces(401)
            //    .Produces<ApiResponse<PostItem>>();

            //routeGroupBuilder.MapPost("/{id:int}/picture", SetPostPicture)
            //    .WithName("SetPostPicture")
            //    .RequireAuthorization()
            //    .Accepts<IFormFile>("multipart/form-data")
            //    .Produces(401)
            //    .Produces<ApiResponse<string>>();

            //routeGroupBuilder.MapPut("/{id:int}", UpdatePost)
            //    .WithName("UpdateAnPost")
            //    .AddEndpointFilter<ValidatorFilter<PostEditModel>>()
            //    .RequireAuthorization()
            //    .Produces(401)
            //    .Produces<ApiResponse<string>>();

            //routeGroupBuilder.MapDelete("/{id:int}", DeletePost)
            //    .WithName("DeleteAnPost")
            //    .RequireAuthorization()
            //    .Produces(401)
            //    .Produces<ApiResponse<string>>();

            return app;
        }

        private static async Task<IResult> GetPosts(
            [AsParameters] PostFilterModel model,
            IBlogRepository blogRepository)
        {
            var postsList = await blogRepository
                .GetPagedPostsAsync(model, model.Title);

            var paginationResult =
                new PaginationResult<PostItem>(postsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetFeaturedPosts(
        int numPosts,
        IBlogRepository blogRepository)
        {
            var postsList = await blogRepository
                .GetPopularPostsAsync(numPosts);

            var paginationResult =
                new PaginationResult<PostItem>(postsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetRandomPosts(
        int numPosts,
        IBlogRepository blogRepository)
        {
            var postsList = await blogRepository
                .GetRandomPostsAsync(numPosts);

            var paginationResult =
                new PaginationResult<PostItem>(postsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetPostDetails(
            int id,
            IBlogRepository blogRepository,
            IMapper mapper)
        {
            var post = await blogRepository.GetCachedPostByIdAsync(id);

            return post == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                $"không tìm thấy bài viết có mã số {id}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<PostItem>(post)));
        }

        private static async Task<IResult> GetPostsBySlug(
            [FromRoute] string slug,
            [AsParameters] PagingModel pagingModel,
            IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                PostSlug = slug,
                PublishedOnly = true
            };

            var postsList = await blogRepository.GetPagedPostsAsync(
                postQuery, pagingModel,
                posts => posts.ProjectToType<PostDto>());
            var paginationResult = new PaginationResult<PostDto>(postsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> AddPost(
            AuthorEditModel model,
            IValidator<AuthorEditModel> validator,
            IAuthorRepository authorRepository,
            IMapper mapper)
        {
            if (await authorRepository
                .IsAuthorSlugExistedAsync(0, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.Conflict, $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }

            var author = mapper.Map<Author>(model);
            await authorRepository.AddOrUpdateAsync(author);

            return Results.Ok(ApiResponse.Success(
                    mapper.Map<AuthorItem>(author), HttpStatusCode.Created));
        }

        private static async Task<IResult> SetPostPicture(
            int id, IFormFile imageFile,
            IAuthorRepository authorRepository,
            IMediaManager mediaManager)
        {
            var imageUrl = await mediaManager.SaveFileAsync(
                imageFile.OpenReadStream(),
                imageFile.FileName, imageFile.ContentType);

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Results.Ok(ApiResponse.Fail(
                   HttpStatusCode.BadRequest, "Không lưu được tập tin"));
            }
            await authorRepository.SetImageUrlAsync(id, imageUrl);

            return Results.Ok(ApiResponse.Success(imageUrl));
        }

        private static async Task<IResult> UpdatePost(
            int id, AuthorEditModel model,
            IValidator<AuthorEditModel> validator,
            IAuthorRepository authorRepository,
            IMapper mapper)
        {
            if (await authorRepository
                    .IsAuthorSlugExistedAsync(id, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.Conflict,
                    $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }

            var author = mapper.Map<Author>(model);
            author.Id = id;

            return await authorRepository.AddOrUpdateAsync(author)
              ? Results.Ok(ApiResponse.Success("Author is updated",
                            HttpStatusCode.NoContent))
              : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                            "Could not find author"));
        }

        private static async Task<IResult> DeletePost(
            int id, IAuthorRepository authorRepository)
        {
            return await authorRepository.DeleteAuthorAsync(id)
              ? Results.Ok(ApiResponse.Success("Author is deleted",
                            HttpStatusCode.NoContent))
              : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                            "Could not find author"));
        }
    }
}

