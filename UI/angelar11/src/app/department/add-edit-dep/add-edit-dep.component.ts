import { Component, OnInit, Input } from '@angular/core';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit-dep',
  templateUrl: './add-edit-dep.component.html',
  styleUrls: ['./add-edit-dep.component.css']
})
export class AddEditDepComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() dep:any;
  Id:string="";
  PublicId:number = 0;
  Name:string="";
  
  ngOnInit(): void {
    this.PublicId = this.dep.PublicId;
    this.Name = this.dep.Name;
    this.Id = this.dep.Id;
  }

  addDepartment()
  {
    var val ={PublicId:0,
              Name:this.Name};
    this.service.addDepartment(val).subscribe(q =>{alert(q.toString())});

  }

  updateDepartment()
  {
    var val ={
      PublicId:this.PublicId,
      Id:this.Id,
      Name:this.Name};
     this.service.updateDepartment(val).subscribe(q =>{alert(q.toString())});
  }

}
