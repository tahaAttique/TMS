using System.ComponentModel.DataAnnotations;

namespace TMS.TicketCategories;

public class UpdateTicketCategoryDto
{
    [Required]
    [StringLength(TicketCategoryConsts.MaxNameLength)]
    public required string Name { get; set; }

    public string? Description { get; set; }
}
