using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Comment;
using api.Dtos;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }

public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto)
{
    if (commentDto.Title == null || commentDto.Content == null)
    {
        throw new ArgumentNullException("Title and Content cannot be null");
    }

    return new Comment
    {
        Title = commentDto.Title,
        Content = commentDto.Content
    };
}

    }
}