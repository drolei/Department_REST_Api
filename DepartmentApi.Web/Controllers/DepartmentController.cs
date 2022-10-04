using DepartmentApi.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DepartmentApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var data = await _departmentService.Get();

            return new JsonResult(data);
        }

        [HttpPost]
        public async Task<JsonResult> Post(DepartmentVm vm)
        {
            await _departmentService.Add(vm.Name);

            return new JsonResult("Added successfully");
        }

        [HttpPut]
        public async Task<JsonResult> Put(DepartmentVm vm)
        {
            await _departmentService.Update(vm);

            return new JsonResult("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _departmentService.Delete(id);
            if (result)
            {
                return new JsonResult("Deleted successfully");
            }
            else
            {
                return new JsonResult("Something went wrong");
            }
        }
    }
}
