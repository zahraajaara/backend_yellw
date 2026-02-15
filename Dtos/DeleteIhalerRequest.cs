using System.ComponentModel.DataAnnotations;

namespace YellowKalam.Api.Dtos
{
    public class DeleteIhalerRequest
    {
        [Required]
        public List<int> IhalerIds { get; set; } = new();
    }

}
