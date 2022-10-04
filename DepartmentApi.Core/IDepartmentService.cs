using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepartmentApi.Core
{
    public interface IDepartmentService
    {
        Task<List<DepartmentVm>> Get();
        Task Add(string name);
        Task Update(DepartmentVm vm);
        Task<bool> Delete(Guid id);
    }

    public class DepartmentVm
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int PublicId { get; set; }
        public string Name { get; set; }

    }
}
