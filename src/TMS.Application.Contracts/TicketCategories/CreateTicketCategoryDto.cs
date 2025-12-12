using System.ComponentModel.DataAnnotations;

namespace TMS.TicketCategories;

public class CreateTicketCategoryDto
{
    [Required]
    [StringLength(TicketCategoryConsts.MaxNameLength)]
    public required string Name { get; set; }

    public string? Description { get; set; }
}
