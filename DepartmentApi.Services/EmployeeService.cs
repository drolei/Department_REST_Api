using DepartmentApi.Core;
using DepartmentApi.Data;
using DepartmentApi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(EmployeeVm vm)
        {
            var employee = new Employee
            {
                PublicId = await Enumerator(),
                DepartmentId = vm.Department.Id,
                Name = vm.Name
            };

            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var emp = await _dbContext.Employees.FindAsync(id);
            if (emp == null) return true;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Employees.Remove(emp);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<List<EmployeeVm>> Get()
        {
            var employees = await _dbContext.Employees.Include(q => q.Department).ToListAsync();
            var vm = employees.Adapt<List<EmployeeVm>>();

            return vm;
        }

        public async Task<EmployeePhotoVm> GetPhoto(Guid id)
        {
            var employeePhoto = await _dbContext.EmployeePhotos.Where(q => q.EmployeeId == id).ToListAsync();
            if (employeePhoto.Count == 0)
            {
                return new EmployeePhotoVm()
                {
                    Id = new Guid("523A1A9D-FA70-45ED-9CA0-1DDF3F037779"),
                    Extension = ".png"
                };
            }
            var photo = employeePhoto.OrderBy(q => q.CreatedOn).LastOrDefault();
            var vm = photo.Adapt<EmployeePhotoVm>();

            vm.Extension = Path.GetExtension(vm.FileName);

            return vm;
        }

        public async Task Update(EmployeeVm vm)
        {
            var emp = await _dbContext.Employees.FirstAsync(q => q.Id == vm.Id);
            emp.Name = vm.Name;
            emp.DepartmentId = vm.Department.Id;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EmployeePhotoVm> UploadPhoto(EmployeePhotoVm vm, Stream stream)
        {
            if (vm.EmployeeId == null)
            {
                throw new ArgumentNullException(nameof(vm.EmployeeId));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var photo = new EmployeePhoto
            {
                FileName = vm.FileName,
                EmployeeId = vm.EmployeeId
            };
            var path = photo.BuildPath();
            var extension = Path.GetExtension(vm.FileName);
            try
            {
               
                using (var streamPath = new FileStream(path, FileMode.Create))
                {
                    await stream.CopyToAsync(streamPath);
                }

            }
            catch (Exception)
            {
                throw new Exception();
            }
            await _dbContext.EmployeePhotos.AddAsync(photo);
            await _dbContext.SaveChangesAsync();

            var photoVm = photo.Adapt<EmployeePhotoVm>();
            photoVm.Extension = extension;

            return photoVm;
        }

        private async Task<int> Enumerator()
        {
            var data = await _dbContext.Employees.ToListAsync();
            int value = 1;
            if (data.Count == 0)
            {
                return value;
            }
            else
            {
                var sortData = data.OrderBy(q => q.PublicId).ToList();
                value = sortData.Last().PublicId;
                value++;
            }

            return value;
        }
    }
}
