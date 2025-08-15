using AutoMapper;
using JWTTraining.Dtos.Blog;
using JWTTraining.Entities;
using JWTTraining.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace JWTTraining.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/blogs")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BlogsController> _logger;

        public BlogsController(IBlogRepository blogRepository, IMapper mapper, ILogger<BlogsController> logger)
        {
            _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogsListDto>>> GetBlogsList()
        {
            var blogsList = await _blogRepository.GetBlogsListAsync();

            var mappedBlogsList = _mapper.Map<IEnumerable<BlogsListDto>>(blogsList);
            return Ok(mappedBlogsList);
        }

        [HttpGet("{blogId}", Name = nameof(GetBlog))]
        public async Task<ActionResult<BlogDto>> GetBlog(int blogId)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(blogId);
            if (blog == null)
            {
                _logger.LogWarning("Blog With Id : {BlogId} Not Founded !", blogId);
                return NotFound();
            }

            var mappedBlog = _mapper.Map<BlogDto>(blog);
            return Ok(mappedBlog);
        }

        [HttpPost]
        public async Task<ActionResult<BlogDto>> AddBlog([FromBody] AddBlogDto addBlogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            var mappedNewBlog = _mapper.Map<Blog>(addBlogDto);

            await _blogRepository.AddBlogAsync(mappedNewBlog);
            await _blogRepository.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetBlog), new
            {
                blogId = mappedNewBlog.Id
            }, mappedNewBlog);
        }

        [HttpPatch("{blogId}")]
        public async Task<ActionResult> PartiallyUpdateBlog(int blogId, JsonPatchDocument<UpdateBlogDto> patchDocument)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(blogId);
            if (blog == null)
            {
                _logger.LogWarning("Blog With Id : {BlogId} Not Founded !", blogId);
                return NotFound();
            }

            var blogToPatch = _mapper.Map<UpdateBlogDto>(blog);

            patchDocument.ApplyTo(blogToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            if (!TryValidateModel(blogToPatch))
            {
                return BadRequest(modelState: ModelState);
            }

            _mapper.Map(blogToPatch, blog);

            await _blogRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{blogId}")]
        public async Task<ActionResult> DeleteBlog(int blogId)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(blogId);
            if (blog == null)
            {
                _logger.LogWarning("Blog With Id : {BlogId} Not Founded !", blogId);
                return NotFound();
            }

            _blogRepository.RemoveBlog(blog);
            await _blogRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
