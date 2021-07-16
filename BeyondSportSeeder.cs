using BeyondSportsApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeyondSportsApi
{
    public class BeyondSportSeeder
    {
        private readonly BeyondSportContext _beyondSportContext;
        public BeyondSportSeeder(BeyondSportContext beyondSportContext)
        {
            _beyondSportContext = beyondSportContext;
        } 
        public void Seed()
        {
            if(_beyondSportContext.Database.CanConnect())
            {
                if(!_beyondSportContext.Teams.Any())
                {
                    InsertSampleData();
                }
            }
        }

        private void InsertSampleData()
        {
            var teams = new List<Team>
            {
                new Team
                {
                    Name = "Juventus",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            FirstName = "Cristiano",
                            LastName = "Ronaldo",
                            Age = 36,
                            Height = 1.87m
                        },
                        new Player
                        {
                            FirstName = "Federico",
                            LastName = "Chiesa",
                            Age = 23,
                            Height = 1.75m
                        }
                    }
                },
                new Team
                {
                    Name = "FC Barcelona",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            FirstName = "Antoine",
                            LastName = "Griezmann",
                            Age = 30,
                            Height = 1.76m
                        },
                        new Player
                        {
                            FirstName = "Lionel",
                            LastName = "Messi",
                            Age = 34,
                            Height = 1.70m
                        }
                    }
                }
            };
            _beyondSportContext.AddRange(teams);
            _beyondSportContext.SaveChanges();
        }
    }
}
