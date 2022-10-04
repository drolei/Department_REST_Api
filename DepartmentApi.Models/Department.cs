using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentApi.Models
{
   public class Department : BaseModel
    {
        public int PublicId { get; set; }

        public string Name { get; set; }
    }
}
