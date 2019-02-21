using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace hpaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {   
            var stringDistance = new StringDistance();
            var  watch =new Stopwatch();
            const long count = 100;
            const int length = 100;
            string comparestring = stringDistance.GenerateRandomString(length);
            var strlist = new string[count];
            var steps = new int[count];
            // prepare string[] for comparison  

            Parallel.For(0, count, i => strlist[i] = stringDistance.GenerateRandomString(length));
            Console.WriteLine("已经生成了" + count + "个长度为" + length + "的字符串");

            watch.Start();
            for (int i = 0; i < count; i++)
            {
                steps[i] = stringDistance.LevenshteinDistance(comparestring, strlist[i]);
            }
            watch.Stop();
            Console.WriteLine("完成非并行计算,耗时(ms)"+watch.ElapsedMilliseconds);
            Console.WriteLine("性能比" + 100000d/watch.ElapsedMilliseconds);
           
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
