using System.ComponentModel.DataAnnotations;

namespace ProjectX.Api.Dtos
{
    public record CreateTodoItemDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public string Todo { get; init; }

    }
}