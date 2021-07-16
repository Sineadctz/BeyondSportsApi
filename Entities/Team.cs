using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeyondSportsApi.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Player> Players { get; set; }
    }
}
