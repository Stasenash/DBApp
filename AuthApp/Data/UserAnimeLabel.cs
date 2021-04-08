using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AuthApp.Data;
using Microsoft.AspNetCore.Identity;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AuthApp.Models
{
    public partial class UserAnimeLabel
    {
        public string UserId { get; set; }
        
        public virtual AppUser User { get; set; }
        
        public long AnimeId { get; set; }
        
        public int LabelId { get; set; }

        public virtual AnimeLabel Label { get; set; }
    }
}
