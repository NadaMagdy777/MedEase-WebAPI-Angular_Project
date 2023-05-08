import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { ScheduleService } from 'src/app/services/Doctor/schedule.service';
import { EditSchedule, Schedule } from 'src/app/sharedClassesAndTypes/doctor/schedule';

@Component({
  selector: 'app-doctor-schedule',
  templateUrl: './doctor-schedule.component.html',
  styleUrls: ['./doctor-schedule.component.css']
})
export class DoctorScheduleComponent {

  id:number = parseInt(this._userAuthService.getLoggedUserId);
  errorMessage: any;

  isSaved:boolean = false;
  isUpdated:boolean = false;
  isDisabled:boolean=false;
  scheduleExist:boolean = false;

  schedule:EditSchedule = {
    id: 0,
    doctorId: this.id,
    isWorking: false,
    weekDay: undefined,
    startTime: undefined,
    endTime: undefined,
    timeInterval: 0,
  };

  ScheduleForm:FormGroup;

  constructor(
    private _scheduleService:ScheduleService,
    private _userAuthService:UserAuthService,
    public router: Router,
    private fb:FormBuilder
  )
  {
    this.ScheduleForm = this.fb.group({

      isWorking:[false,[Validators.required]],
      weekDay:[,[Validators.required]],
      startTime:[,[Validators.required]],
      endTime:[,[Validators.required]],
      timeInterval:[10,[Validators.required]],
    });

    this.ScheduleForm.get('isWorking')?.valueChanges.subscribe((data) => {
      this.schedule.isWorking = data;
    });
    this.ScheduleForm.get('weekDay')?.valueChanges.subscribe((data) => {
      this.schedule.weekDay = data;
    });
    this.ScheduleForm.get('startTime')?.valueChanges.subscribe((data) => {      
      this.schedule.startTime = data;
    });
    this.ScheduleForm.get('endTime')?.valueChanges.subscribe((data) => {
      this.schedule.endTime = data;
    });
    this.ScheduleForm.get('timeInterval')?.valueChanges.subscribe((data) => {
      this.schedule.timeInterval = data;
    });

  }

  get isWorking()
  {
    return this.ScheduleForm.get('isWorking');
  }
  get weekDay()
  {
    return this.ScheduleForm.get('weekDay');
  }
  get startTime()
  {
    return this.ScheduleForm.get('startTime');
  }
  get endTime()
  {
    return this.ScheduleForm.get('endTime');
  }
  get timeInterval()
  {
    return this.ScheduleForm.get('timeInterval');
  }

  LoadFormData(): void {

    this.ScheduleForm.patchValue({
      isWorking:this.schedule.isWorking,
      weekDay:this.schedule.weekDay,
      startTime:this.schedule.startTime,
      endTime:this.schedule.endTime,
      timeInterval:this.schedule.timeInterval,
    });
  }
  
  Save():void {

    if(this.scheduleExist){
      this._scheduleService.UpdateSchedules(this.schedule.id ,this.schedule)
      .subscribe(response => {
        console.log(response)
      });
      this.scheduleExist = false;
      this.isUpdated = true;
      setTimeout(()=>{
        this.isUpdated = false;
      },3000)
    } 
    else if(this.schedule.weekDay != null){
      
      this._scheduleService.AddSchedule(this.schedule)
      .subscribe(response => {
        console.log(response)
      });

      this.isSaved = true;
      setTimeout(()=>{
        this.isSaved = false;
      },3000)
    }
    this.ScheduleForm.reset();
    
  } 
  Search():void {
    this.scheduleExist = false;
    
    const date = new Date(this.ScheduleForm.get('weekDay')?.value);
    const formattedDate = new DatePipe('en-US').transform(date, 'M/d/yyyy');

    this._scheduleService.GetAllDoctorSchedules(this.id).subscribe({
      next: (data: any) => {
        (data.data).forEach((element: any) => {
          if((element.weekDay).includes(formattedDate)){
            
            this.schedule = element;
            this.schedule.isWorking = this.isDisabled;
            this.schedule.weekDay = new DatePipe('en-US').transform(date, 'yyyy-MM-dd'); 
            this.scheduleExist = true;
            this.LoadFormData();
          }
          
        })
      }, 
      error: (error: any) => this.errorMessage = error 
    });  
    if(this.scheduleExist==false){
      this.schedule = 
      {
        id: 0,
        doctorId: this.id,
        isWorking: this.isDisabled,
        weekDay: new DatePipe('en-US').transform(date, 'yyyy-MM-dd'),
        startTime: undefined,
        endTime: undefined,
        timeInterval: 10,
      }
      this.LoadFormData();
    }
  }
  Cancel() : void { 
    if(window.confirm('Are you sure, you want to cancel, you are about to lose the new data?')){
      
      if(this.scheduleExist){
        this.LoadFormData(); 
      }
      else{
        this.ScheduleForm.reset();
      }
    }
  }
  Switch(event:any){
    this.isDisabled=!this.isDisabled;
    this.isWorking?.setValue(!this.isWorking.value)
  }
}
