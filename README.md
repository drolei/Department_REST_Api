# DepartmentApi
Angular&amp;Asp.net core project 

To get started with this project :
1. create database (Entity framework Core helps)

2.change DefaultPath in the DepartmentApi\DepartmentApi.Core IEmployeeService.cs class EmployeePhotoVm
This needs for Employee Photos

2.1 copy photo from DepartmentApi\DepartmentApi.Web\Photos to your Default folder for Photos
This needs for Employees who doesn't have their photo

3. Launch DepartmentApi then check your url and compare its with ApiUrl & PhotoUrl in the DepartmentApi\UI\angelar11\src\app shared.service.ts

3.1 if your url is the same, you can Launch Angular FrontEnd (Use 'cmd' in app folder url then use command 'ng serve --open' in console )

3.2 if not, change ApiUrl & PhotoUrl to your localhost (example https://localhost:5001(your localhOst)/Photos/) and do clause 3.1

p.s. server and client run separately

gl hf
