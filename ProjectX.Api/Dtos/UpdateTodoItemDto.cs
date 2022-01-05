using System.ComponentModel.DataAnnotations;

namespace ProjectX.Api.Dtos
{
    public record UpdateTodoItemDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public string Todo { get; init; }

    }
}