using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BeyondSportsApi.Models
{
    public class TeamDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public List<PlayerDto> Players { get; set; }
    }
}
