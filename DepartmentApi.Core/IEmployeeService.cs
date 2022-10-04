using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DepartmentApi.Core
{
    public interface IEmployeeService
    {
        Task<List<EmployeeVm>> Get();
        Task Add(EmployeeVm vm);
        Task Update(EmployeeVm vm);
        Task<bool> Delete(Guid id);
        Task<EmployeePhotoVm> UploadPhoto(EmployeePhotoVm vm, Stream stream);
        Task<EmployeePhotoVm> GetPhoto(Guid id);
    }

    public class EmployeeVm
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int PublicId { get; set; }
        public DepartmentVm Department { get; set; }
        public string Name { get; set; }

        public string CreatedOnStr { get; set; }
    }

    public class EmployeePhotoVm
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid EmployeeId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string DefaultPath { get { return @"A:\Photos"; }
            set { DefaultPath = value; }
        }

    }
}
