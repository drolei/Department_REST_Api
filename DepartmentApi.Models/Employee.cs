using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentApi.Models
{
   public class Employee : BaseModel
    {
        public int PublicId { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public string Name { get; set; }


    }
}
