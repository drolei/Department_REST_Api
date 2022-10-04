import { Component, OnInit } from '@angular/core';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {

  constructor(private service:SharedService) { }

  DepartmentList:any=[];
  ModalTitle:string= "";
  ActivateAddEditDepComp:boolean=false;
  dep:any;

  DepartmentPublicIdFilter:string="";
  DepartmentNameFilter:string="";
  DepartmenListWithoutFilter: any=[];

  ngOnInit(): void {
    this.refreshDepList();
  }

  addClick()
  {
    this.dep={
      PublicId:0,
      Name:""
    };
    this.ModalTitle ="Add Department";
    this.ActivateAddEditDepComp = true;
  }

  editClick(item:any)
  {
    this.dep=item;
    this.ModalTitle ="Edit Department";
    this.ActivateAddEditDepComp = true;
  }

  closeClick()
  {
    this.ActivateAddEditDepComp = false;
    this.refreshDepList();
  }

  deleteClick(item:any)
  {
   if(confirm('Are you sure? '))
   {
    this.dep={Id:item.Id};
    this.service.deleteDepartment(this.dep).subscribe(q =>{alert(q.toString());
    this.refreshDepList(); });
   }
  }

  refreshDepList(){
    this.service.getDepList().subscribe(data =>{
      this.DepartmentList = data;
      this.DepartmenListWithoutFilter = data;
    });
  }

  FilterFn()
  {
    var departmentPublicIdFilter = this.DepartmentPublicIdFilter;
    var departmentNameFilter = this.DepartmentNameFilter;

    this.DepartmentList = this.DepartmenListWithoutFilter.filter(function (el : any) {
      return el.PublicId.toString().toLowerCase().includes(departmentPublicIdFilter
        .toString().trim().toLowerCase())&&
      el.Name.toString().toLowerCase().includes(departmentNameFilter
        .toString().trim().toLowerCase())
    });
  }

  sortResult(prop:any,asc:any){
    this.DepartmentList= this.DepartmenListWithoutFilter.sort(function (a:any,b:any){
      if(asc){
       return (a[prop]>b[prop])?1 : ((a[prop]<b[prop]) ?-1 :0);
      }
      else{   
       return (b[prop]>a[prop])?1 : ((b[prop]<a[prop]) ?-1 :0);
      }
      });
     
  }
}
