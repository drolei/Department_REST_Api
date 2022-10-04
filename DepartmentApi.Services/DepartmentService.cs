using DepartmentApi.Core;
using DepartmentApi.Data;
using DepartmentApi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(string name)
        {
            var department = new Department
            {
                PublicId = await Enumerator(),
                Name = name
            };

            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var dep = await _dbContext.Departments.FindAsync(id);
            if (dep == null) return true;
            //ToDO check relationships with employees
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Departments.Remove(dep);
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

        public async Task<List<DepartmentVm>> Get()
        {


            var departments = await _dbContext.Departments.ToListAsync();
            var vm = departments.Adapt<List<DepartmentVm>>();

            return vm;
        }

        public async Task Update(DepartmentVm vm)
        {
            var dep = await _dbContext.Departments.FirstAsync(q => q.Id == vm.Id);
            dep.Name = vm.Name;
            await _dbContext.SaveChangesAsync();
        }

        private async Task<int> Enumerator()
        {
            var data = await _dbContext.Departments.ToListAsync();
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
