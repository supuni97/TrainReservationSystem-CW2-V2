using System.ComponentModel.DataAnnotations;

namespace SpecialRequest.Api.Models;

public class SpecialRequestModel
{
    public int Id { get; set; }

    [Required]
    public int BookingId { get; set; }

    [Required]
    [StringLength(100)]
    public string RequestType { get; set; } = string.Empty;

    [Required]
    [StringLength(300)]
    public string Description { get; set; } = string.Empty;

    public DateTime RequestDate { get; set; } = DateTime.Today;

    public string Status { get; set; } = "Pending";
}