using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Dtos
{
  public record CreateItemDto
  {
    [Required]
    public string Name { get; init; }
    [Required]
    [Range(0, 9999)]
    public decimal Price { get; init; }
  }
}