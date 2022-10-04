import { Component, OnInit } from '@angular/core';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private service:SharedService) { }

  EmployeeList:any=[];
  ModalTitle:string= "";
  ActivateAddEditEmpComp:boolean=false;
  emp:any;

  PublicIdFilter:string="";
  NameFilter:string="";
  EmployeeListWithoutFilter: any=[];

  ngOnInit(): void {
    this.refreshEmpList();
  }

  addClick()
  {
    this.emp={
      PublicId:0,
      Name:"",
      PhotoFileName:"anonymous.png"
    };
    this.ModalTitle ="Add Employee";
    this.ActivateAddEditEmpComp = true;
  }

  editClick(item:any)
  {
    this.emp=item;
    this.ModalTitle ="Edit Employee";
    this.ActivateAddEditEmpComp = true;
  }

  closeClick()
  {
    this.ActivateAddEditEmpComp = false;
    this.refreshEmpList();
  }

  deleteClick(item:any)
  {
   {
    this.service.deleteEmployee(item.Id).subscribe(q =>{alert(q.toString());
    this.refreshEmpList(); });
   }
  }

  refreshEmpList(){
    this.service.getEmpList().subscribe(data =>{
      this.EmployeeList = data;
      this.EmployeeListWithoutFilter = data;
    }
      )
  }
  
  FilterFn()
  {
    var publicIdFilter = this.PublicIdFilter;
    var nameFilter = this.NameFilter;

    this.EmployeeList = this.EmployeeListWithoutFilter.filter(function (el : any) {
      return el.PublicId.toString().toLowerCase().includes(publicIdFilter
        .toString().trim().toLowerCase())&&
      el.Name.toString().toLowerCase().includes(nameFilter
        .toString().trim().toLowerCase())
    });
  }

  sortResult(prop:any,asc:any){
    this.EmployeeList= this.EmployeeListWithoutFilter.sort(function (a:any,b:any){
      if(asc){
       return (a[prop]>b[prop])?1 : ((a[prop]<b[prop]) ?-1 :0);
      }
      else{   
       return (b[prop]>a[prop])?1 : ((b[prop]<a[prop]) ?-1 :0);
      }
      });
     
  }

}
