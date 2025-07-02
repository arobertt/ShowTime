using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.DTOs
{
    public class ArtistUpdateDto : ArtistCreateDto
    {
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        public override string Name { get; set; } = string.Empty;

        [MinLength(3, ErrorMessage = "Genre must be at least 3 characters")]
        public override string Genre { get; set; } = string.Empty;

        [RegularExpression(@"^https://.*", ErrorMessage = "Image URL must be valid")]
        public override string ImageUrl { get; set; } = string.Empty;
    }
}
