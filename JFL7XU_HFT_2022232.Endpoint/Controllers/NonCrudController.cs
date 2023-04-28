using JFL7XU_HFT_2022232.Logic.Interfaces;
using JFL7XU_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JFL7XU_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NonCrudController : ControllerBase
    {
        INonCrudLogic logic;
        public NonCrudController(INonCrudLogic logic)
        {
            this.logic = logic;
        }

        // GET: <NonCrudController>
        [HttpGet("{year}")]
        public IEnumerable<Starship> ListShips_WhichBuiltAfter(int year)
        {
            return logic.ListShips_WhichBuiltAfter(year);
        }
        [HttpGet("{quantity}")]
        public IEnumerable<Hangar> ListHangars_WithShipsMoreThan(int quantity)
        {
            return logic.ListHangars_WithShipsMoreThan(quantity);
        }
        [HttpGet("{quantity}")]
        public IEnumerable<Hangar> ListHangars_WithShipsLessThan(int quantity)
        {
            return logic.ListHangars_WithShipsLessThan(quantity);
        }
        [HttpGet("{year}")]
        public IEnumerable<Owner> ListOwners_OlderThan(int year)
        {
            return logic.ListOwners_OlderThan(year);
        }
        [HttpGet("{year}")]
        public IEnumerable<Owner> ListOwners_YoungerThan(int year)
        {
            return logic.ListOwners_YoungerThan(year);
        }
        [HttpGet("{year}/{quantity}")]
        public IEnumerable<Owner> ListOwners_YoungerAndHasMoreShipsThan(int year, int quantity)
        {
            return logic.ListOwners_YoungerAndHasMoreShipsThan(year, quantity);
        }
        [HttpGet]
        public IEnumerable<OwnershipStatistics> ListStatistics()
        {
            return logic.ListStatistics();
        }
    }
}
