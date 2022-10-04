import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

readonly ApiUrl ="https://localhost:5001/api";
readonly PhotoUrl ="https://localhost:5001/Photos/";  

  constructor(private http:HttpClient) { }

  getDepList():Observable<any[]>
  {
    return this.http.get<any[]>(this.ApiUrl+'/department');
  }

  addDepartment(val:any)
  {
    return this.http.post(this.ApiUrl+'/Department',val);
  }

  updateDepartment(val:any)
  {
    return this.http.put(this.ApiUrl+'/Department',val);
  }

  deleteDepartment(val:any)
  {
    return this.http.delete(this.ApiUrl+'/Department/'+ val.Id);  
  }

  getEmpList():Observable<any[]>
  {
    return this.http.get<any>(this.ApiUrl+'/Employee');
  }

  addEmployee(val:any)
  {
    return this.http.post(this.ApiUrl+'/Employee',val);
  }

  updateEmployee(val:any)
  {
    return this.http.put(this.ApiUrl+'/Employee',val);
  }

  deleteEmployee(val:any)
  {
    return this.http.delete(this.ApiUrl+'/Employee/'+val);
  }

  uploadPhoto(val:any,id:any)
  {
    return this.http.post(this.ApiUrl+'/Employee/'+id+'/UploadPhoto',val);
  }

  getPhoto(id:any):Observable<any>
  {
    return this.http.get<any>(this.ApiUrl+'/Employee/GetPhoto/'+id);
  }
  
}
