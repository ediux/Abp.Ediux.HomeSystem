using System;

namespace Ediux.HomeSystem.Features.Blogs.DTOs
{
    public class BlogPostSearchRequestDto : BlogSearchRequestDto
    {
        /// <summary>
        /// 部落格識別碼
        /// </summary>
        public Guid BlogId { get; set; }

        /// <summary>
        /// 以文章後短網址搜尋
        /// </summary>
        public string SearchBySLUG { get; set; }
    }
}
