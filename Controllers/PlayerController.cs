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
    public class PlayerController : ControllerBase
    {
        private readonly BeyondSportContext _beyondSportContext;
        private readonly IMapper _mapper;

        public PlayerController(BeyondSportContext beyondSportContext, IMapper mapper)
        {
            _beyondSportContext = beyondSportContext;
            _mapper = mapper;
        }

        //Get all players
        [HttpGet]
        public ActionResult<List<PlayerDto>> GetAll()
        {
            var players = _beyondSportContext.Players.ToList();
            if(players == null)
            {
                return NotFound("No players found.");
            }
            var playerDto = _mapper.Map<List<PlayerDto>>(players);
            return Ok(playerDto);
        }

        //Get player by id
        [HttpGet("{id}")]
        public ActionResult<PlayerDto> GetPlayer(int id)
        {
            var player = _beyondSportContext.Players
               .FirstOrDefault(t => t.Id == id);

            if (player == null)
            {
                return NotFound("Player not found.");
            }

            var playerDto = _mapper.Map<PlayerDto>(player);
            return Ok(playerDto);
        }

        //Get all players for a team by id
        [HttpGet("teamplayers/{id}")]
        public ActionResult<List<PlayerDto>> GetPlayersPerTeam(int id)
        {
            var teams = _beyondSportContext.Teams
                .Include(t => t.Players)
                .FirstOrDefault(t => t.Id == id);

            if (teams == null)
            {
                return NotFound("No teams found.");
            }

            var playerDto = _mapper.Map<List<PlayerDto>>(teams.Players);
            if(playerDto.Count == 0)
            {
                return NotFound("The team do not have players yet.");
            }
            return Ok(playerDto);
        }
        //add a player
        [HttpPost]
        public ActionResult Post([FromBody] PlayerDto playerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            var player = _mapper.Map<Player>(playerDto);
            _beyondSportContext.Players.Add(player);
            _beyondSportContext.SaveChanges();

            var key = player.Id;
            return Created($"api/player/" + key, player);
        }
        //update a player
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] PlayerDto playerDto)
        {
            var player = _beyondSportContext.Players.FirstOrDefault(t => t.Id == id);
            if (player == null)
            {
                return NotFound("The player you are attempting to update could not be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            player.FirstName = playerDto.FirstName;
            player.LastName = playerDto.LastName;
            player.Age = playerDto.Age;
            player.Height = playerDto.Height;
            player.TeamId = playerDto.TeamId;

            _beyondSportContext.SaveChanges();

            return Ok("Update Successfull.");

        }

        //remove a player
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var player = _beyondSportContext.Players.FirstOrDefault(p => p.Id == id);
            if(player == null)
            {
                return NotFound("The player you are attempting to remove could not be found.");
            }

            _beyondSportContext.Players.Remove(player);
            _beyondSportContext.SaveChanges();

            return Ok("The player was successfylly removed.");

        }
    }
}
