﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShowTime.BusinessLogic.Miscellaneous;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.DTOs
{
    public class FestivalCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required")]
        [MinLength(3, ErrorMessage = "Location must be at least 3 characters")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation.NotPastDate(ErrorMessage = "Start date cannot be in the past")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation.EndDateAfterStartDate("StartDate", ErrorMessage = "End date must be after start date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Splash art is required")]
        [RegularExpression(@"^https://.*", ErrorMessage = "Splash art must be valid")]
        public string SplashArt { get; set; } = string.Empty;

        [BindRequired]
        [Range(1, Int32.MaxValue - 1, ErrorMessage ="Invalid capacity field")]
        public int Capacity { get; set; }
        public IList<int> ArtistIds { get; set; } = new List<int>();
    }
}
