using JFL7XU_HFT_2022232.Endpoint.Services;
using JFL7XU_HFT_2022232.Logic.Interfaces;
using JFL7XU_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JFL7XU_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HangarController : ControllerBase
    {
        IHangarLogic logic;
        IHubContext<SignalRHub> hub;
        public HangarController(IHangarLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<OwnerController>
        [HttpGet]
        public IEnumerable<Hangar> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]
        public Hangar Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<OwnerController>
        [HttpPost]
        public void Create([FromBody] Hangar value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("HangarCreated", value);
        }

        // PUT api/<OwnerController>/5
        [HttpPut]
        public void Update([FromBody] Hangar value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("HangarUpdated", value);
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deleted = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("HangarDeleted", deleted);
        }
    }
}
