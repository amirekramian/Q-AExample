using Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Base
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorization]
    public class BaseController<T> : ControllerBase, IBaseController<T> where T : BaseEntity
    {
        private readonly IBaseController<T> _business;
            public  BaseController(IBaseController<T> business)=>
            _business=business;
        [HttpPost]
        public async Task<string?> CreateAsync(T t, CancellationToken cancellationToken)
        {
            await _business.CreateAsync(t, cancellationToken);
            return "Created";
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<string> DeleteAsync(int Id, CancellationToken cancellationToken)=>
            await _business.DeleteAsync(Id, cancellationToken);
       
        [HttpGet]
        //public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
        [HttpOptions]
        public void Option()
        {
            Response.Headers.Add("Allow", "POST,PUT,DELETE,GET,HEAD,OPTIONS");
        }
        [HttpPut]
        public async Task<string?> UpdateAsync(T t, CancellationToken cancellationToken)=>
            await _business.UpdateAsync(t, cancellationToken);
            
        
            
        
    }
}
