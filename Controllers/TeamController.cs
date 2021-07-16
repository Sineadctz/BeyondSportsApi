using AutoMapper;
using BeyondSportsApi.Entities;
using BeyondSportsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeyondSportsApi.Controllers
{
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly BeyondSportContext _beyondSportContext;
        private readonly IMapper _mapper;

        public TeamController(BeyondSportContext beyondSportContext, IMapper mapper)
        {
            _beyondSportContext = beyondSportContext;
            _mapper = mapper;
        }
        //Get all teams
        [HttpGet]
        public ActionResult<List<TeamDto>> Get()
        {
            var teams = _beyondSportContext.Teams.ToList();
            if (teams == null)
            {
                return NotFound("No teams found.");
            }
            var teamDtos = _mapper.Map<List<TeamDto>>(teams);
            return Ok(teamDtos);
        }

        //Get team by id
        [HttpGet("{id}")]
        public ActionResult<TeamDto> GetTeam(int id)
        {
            var teams = _beyondSportContext.Teams
                .FirstOrDefault(t => t.Id == id);

            if (teams == null)
            {
                return NotFound("Team not found.");
            }

            var teamDto = _mapper.Map<TeamDto>(teams);
            return Ok(teamDto);
        }

        //Get team and all players from a team
        [HttpGet("teamplayers/{id}")]
        public ActionResult<TeamDto> Get(int id)
        {
            var teams = _beyondSportContext.Teams
                .Include(t => t.Players)
                .FirstOrDefault(t => t.Id == id);

            if(teams == null)
            {
                return NotFound("No teams found.");
            }

            var teamDto = _mapper.Map<TeamDto>(teams);
            if(teamDto == null)
            {
                return NotFound("Team and players not found.");
            }
            return Ok(teamDto);
        }

        [HttpPost]
        public ActionResult Post([FromBody]TeamDto teamDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var team = _mapper.Map<Team>(teamDto);

            _beyondSportContext.Teams.Add(team);
            _beyondSportContext.SaveChanges();

            var key = team.Id;
            return Created("api/team/" + key, team);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TeamDto teamDto)
        {
            var teams = _beyondSportContext.Teams.FirstOrDefault(t => t.Id == id);
            if (teams == null)
            {
                return NotFound("The team you are attempting to update could not be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            teams.Name = teamDto.Name;

            _beyondSportContext.SaveChanges();

            return Ok("Update Successfull.");

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id, [FromBody] TeamDto teamDto)
        {
            var teams = _beyondSportContext.Teams.FirstOrDefault(t => t.Id == id);
            if (teams == null)
            {
                return NotFound("Thea team you are attempting to remove could not be found.");
            }

            _beyondSportContext.Remove(teams);
            _beyondSportContext.SaveChanges();

            return Ok("The team was successfully removed.");

        }


    }
}
