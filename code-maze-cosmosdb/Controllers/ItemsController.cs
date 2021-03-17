using code_maze_cosmosdb.Models;
using code_maze_cosmosdb.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace code_maze_cosmosdb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ItemsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }

        // GET api/items
        [HttpGet]
        public async Task<IEnumerable<Item>> List()
        {
            return await _cosmosDbService.GetMultipleAsync("SELECT * FROM c");
        }

        // GET api/items/5
        [HttpGet("{id}")]
        public async Task<Item> Get(string id)
        {
            return await _cosmosDbService.GetAsync(id);
        }

        // POST api/items
        [HttpPost]
        public async Task<string> Create([FromBody] Item item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(item);
            return item.Id;
        }

        // PUT api/items/5
        [HttpPut("{id}")]
        public async Task<Item> Edit([FromBody] Item item)
        {
            await _cosmosDbService.UpdateAsync(item.Id, item);
            return item;
        }

        // DELETE api/items/5
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            await _cosmosDbService.DeleteAsync(id);
        }
    }
}

