using DepartmentApi.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var data = await _employeeService.Get();
            foreach (var item in data) 
            {
                item.CreatedOnStr = item.CreatedOn.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);
            }

            return new JsonResult(data);
        }

        [HttpPost]
        public async Task<JsonResult> Post(EmployeeVm vm)
        {
            await _employeeService.Add(vm);

            return new JsonResult("Added successfully");
        }

        [HttpPut]
        public async Task<JsonResult> Put(EmployeeVm vm)
        {
            await _employeeService.Update(vm);

            return new JsonResult("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _employeeService.Delete(id);
            if (result)
            {
                return new JsonResult("Deleted successfully");
            }
            else
            {
                return new JsonResult("Something went wrong");
            }
        }

        [HttpPost("{id}/UploadPhoto")]
        public async Task<JsonResult> UploadPhoto([FromRoute] Guid id)
        {
            EmployeePhotoVm vm = new EmployeePhotoVm();
            try
            {
                vm.EmployeeId = id;
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                vm.FileName = postedFile.FileName;
                var photo = new EmployeePhotoVm();
                await using (var stream = postedFile.OpenReadStream())
                {

                    photo = await _employeeService.UploadPhoto(vm, stream);
                }

                // var response = new UploadRatingCommentPhotoResponse { Id = id };
                return new JsonResult(photo);
            }
            catch (Exception)
            {
                return new JsonResult("something went wrong");
            }
        }

        [Route("GetPhoto/{id}")]
        [HttpGet()]
        public async Task<JsonResult> GetPhoto([FromRoute] Guid id)
        {
            var data = await _employeeService.GetPhoto(id);

            return new JsonResult(data);
        }

    }
}
