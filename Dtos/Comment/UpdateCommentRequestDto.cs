namespace api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        
        public string? Title { get; set; } = string.Empty;
        public string? Content { get; set; } = string.Empty;

        public UpdateCommentRequestDto(string? title, string? content)
        {
            Title = title;
            Content = content;
        }

        public UpdateCommentRequestDto() { }
    }
}