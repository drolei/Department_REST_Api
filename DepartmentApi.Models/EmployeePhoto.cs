using DepartmentApi.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DepartmentApi.Models
{
   public class EmployeePhoto : BaseModel
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string FileName { get; set; }

        public string BuildPath()
        {
            var extension = Path.GetExtension(FileName);
            var strId = Id.ToString();
            // var path = @$"A:\Photos{strId}{extension}";
            var path = Path.Combine(new EmployeePhotoVm().DefaultPath, $"{strId}{extension}");
            return path;
        }
    }
}
