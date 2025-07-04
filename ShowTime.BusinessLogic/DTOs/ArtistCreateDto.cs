﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.DTOs
{
    public class ArtistCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        public virtual string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required")]
        [MinLength(3, ErrorMessage = "Genre must be at least 3 characters")]
        public virtual string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image URL is required")]
        [RegularExpression(@"^https://.*", ErrorMessage = "Image URL must be valid")]
        public virtual string ImageUrl { get; set; } = string.Empty;
    }
}
