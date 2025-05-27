using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters or more.")]
        [MaxLength(280, ErrorMessage = "Title can not exceed 280 characters.")]

        public string? Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters or more.")]
        [MaxLength(280, ErrorMessage = "Content can not exceed 280 characters.")]
        public string? Content { get; set; } = string.Empty;

        public UpdateCommentRequestDto(string? title, string? content)
        {
            Title = title;
            Content = content;
        }

        public UpdateCommentRequestDto() { }
    }
}