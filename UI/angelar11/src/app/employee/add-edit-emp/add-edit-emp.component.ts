
import { Component, OnInit, Input } from '@angular/core';
import {SharedService} from 'src/app/shared.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.css']
})
export class AddEditEmpComponent implements OnInit {

  constructor(private service:SharedService,private sanitizer: DomSanitizer) { }

  @Input() emp:any;
  Id:any=null;
  PublicId:number = 0;
  Name:string="";
  Department:any="";
  Photo:any;
  PhotoPath:string="";

  DepartmentsList:any[] =[];

  ngOnInit(): void {
    this.loadData();
  }

   loadData()
   {
     this.service.getDepList().subscribe((data:any)=>
     {
     this.DepartmentsList=data;

     this.Id = this.emp.Id;
     this.Name = this.emp.Name;
     this.PublicId = this.emp.PublicId;

     this.Department = this.emp.Department;
     if(this.emp.Department == null)
     {
       this.Department ={Name:""};
     }
     }); 
     
     if(this.emp.Id != null)
     {
       this.service.getPhoto(this.emp.Id).subscribe((q:any) =>
       { this.Photo = q; 
        this.PhotoPath = this.service.PhotoUrl+q.Id.toString()+q.Extension;
        });

      
     }
   }

  addEmployee()
  {
    var val ={
              PublicId:0,
              Name:this.Name,
              Department:this.Department,
              };
    this.service.addEmployee(val).subscribe(q =>{alert(q.toString())});

  }  

  updateEmployee()
  {
    var dep = this.Department ;
    var val ={
      Id:this.Id,
      PublicId:this.PublicId,
      Name:this.Name,
      Department:this.Department,
      };
     this.service.updateEmployee(val).subscribe(q =>{alert(q.toString())});
  }

  uploadPhoto(event:any)
  {
   var file = event.target.files[0];
   const formData:FormData= new FormData();
   formData.append('uploadedFile',file,file.name);

   this.service.uploadPhoto(formData,this.emp.Id).subscribe((data:any)=> {
     this.Photo = data;
     this.PhotoPath = this.service.PhotoUrl+data.Id.toString()+data.Extension;
   });
  }

  

}
