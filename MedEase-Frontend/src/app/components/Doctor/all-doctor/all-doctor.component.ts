import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DoctorService } from 'src/app/services/Doctor/doctor.service';
import { Doctor } from 'src/app/sharedClassesAndTypes/Doctor/Doctor';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ISubSpecialty } from 'src/app/sharedClassesAndTypes/Doctor/isub-specialty';
import { SpecialtiesService } from 'src/app/services/specialities/specialities.service';
import { ImageService } from 'src/app/services/image.service';
import { ScheduleService } from 'src/app/services/Doctor/schedule.service';
import { EditSchedule, Schedule } from 'src/app/sharedClassesAndTypes/doctor/schedule';
import { doctorWorking } from 'src/app/sharedClassesAndTypes/Doctor/doctorworking';
@Component({
  selector: 'app-all-doctor',
  templateUrl: './all-doctor.component.html',
  styleUrls: ['./all-doctor.component.css']
})
export class AllDoctorComponent implements OnInit{
DoctorList!:Doctor[]
filteredDoctorList:Doctor[]=this.DoctorList
errorMessage: any;
genderFilter:number[]=[]
feesFilter:number=0
workstatus:doctorWorking={
  today: false,
  tommorow: false
}
subspecialityFilter:number[]=[]
selectedSorting:any=0
specialityId:number=0
cityName:string="Egypt"
regionName:string="All"
Doctorname:string="All"
subspiciality:ISubSpecialty[]=[]
filterExamination:number[]=[]

constructor(private DoctorService:DoctorService,private router:Router ,
  private route:ActivatedRoute,private specialityService: SpecialtiesService,
  private _imageService:ImageService,
  private DoctorSchduale:ScheduleService){
   
    
    

}
ngOnInit(): void {
  this.DoctorService.GetAllDoctors().subscribe({
    next:data=>{
      let dataJson = JSON.parse(JSON.stringify(data))
      this.DoctorList=dataJson.data
      this.specialityId= this.route.snapshot.params['speciality']
      this.cityName=this.route.snapshot.params['city']
      this.regionName=this.route.snapshot.params['region']
      this.Doctorname=this.route.snapshot.params['name']
      this.specialityService.GetSubspicilityBySpecialityId(this.specialityId).subscribe({
        next:data=>{
          let dataJson = JSON.parse(JSON.stringify(data))
           this.subspiciality=dataJson.data


        },
        error:error=>this.errorMessage=error
      })
      this.fileterDoctorWhenLoadingPage()
      this.filteredDoctorList=this.DoctorList
      this.filteredDoctorList.forEach((doctor:Doctor)=>{
        doctor.profilePicture=this._imageService.base64ArrayToImage(doctor.profilePicture)
        this.getschduale(doctor)
      })

      

    },
    error:error=>this.errorMessage=error
  })
  
 
  
  
}

getschduale(doctor:Doctor){
  doctor.workingstatus=this.workstatus

  var doctorschduale=this.DoctorSchduale.GetAllDoctorSchedules(doctor.id)
  doctorschduale.forEach((s:EditSchedule[])=>{
    let dataJson = JSON.parse(JSON.stringify(s))
      let doctorSchduale =dataJson.data
      console.log(doctorSchduale)
    if(doctorSchduale.length>0){
      doctorSchduale.forEach((secduale:EditSchedule)=>{
        secduale.weekDay=(secduale.weekDay).split(' ')[0]
        if(new Date(secduale.weekDay).getDay()==new Date().getDay()){
         console.log(doctor.name)
          doctor.workingstatus.today=true


        }
        else{
          doctor.workingstatus.today=false
        }
        if(new Date(secduale.weekDay).getDay()==new Date().getDay()+1){
          console.log(doctor.name)

          doctor.workingstatus.tommorow=true
        }
        else{
          doctor.workingstatus.tommorow=false
                }
      }
      ) 
    }
  
})

  

}
Doctorfilter(){
  this.filteredDoctorList=this.DoctorList
  if(this.genderFilter.length>0){
    this.filterDoctorByGender()

  }
  if(this.subspecialityFilter.length>0){
    this.filterDoctorBySubspeciality()

  }
  if(this.feesFilter>0){
    this.filterDoctorByFees()
  }
  if(this.filterExamination.length>0){
    console.log("filterby Examination")
    this.filterDoctorByExaminationDate()

  }

}

filterDoctorByExaminationDate(){
  if(this.filterExamination.includes(1)){
    console.log("filterby today")

    this.filteredDoctorList=this.filteredDoctorList.filter((doctor:Doctor)=>{
      console.log(doctor.workingstatus.today)
      return doctor.workingstatus.today===true
      
    });

  }
  if(this.filterExamination.includes(2)){
    console.log("filterby tommor")

    this.filteredDoctorList=this.filteredDoctorList.filter((doctor:Doctor)=>{
      return doctor.workingstatus.tommorow==true
      
    });
  }
  

}

onExaChange(examination:number,event:any){
  if(event.target.checked){
    this.filterExamination.push(examination)
  }
  else{
    this.filterExamination= this.filterExamination.filter((num:number)=>num!=examination)
  }
  this.Doctorfilter() 

}
filterDoctorByGender(){
  this.filteredDoctorList=this.filteredDoctorList.filter((doctor:Doctor)=>{
    return this.genderFilter.includes(doctor.gender)
  });
}

filterDoctorBySubspeciality(){
  this.filteredDoctorList=this.filteredDoctorList.filter((doctor:Doctor)=>{
   if( doctor.doctorSubspiciality.filter((sub:ISubSpecialty)=>{
       return  this.subspecialityFilter.includes(sub.id)
    }).length>0){
      return true
    }
    else{
      return false
    }
    

  });
  
  
}

filterDoctorByFees(){
  if(this.feesFilter===1){
    this.filteredDoctorList=this.filteredDoctorList.filter((doctor:Doctor)=>doctor.fees<50
    );
  }
  else if(this.feesFilter===2){
    this.filteredDoctorList=this.filteredDoctorList.filter((doctor:Doctor)=>doctor.fees>=50 && doctor.fees<100);

  }
  else if(this.feesFilter===3){
    this.filteredDoctorList=this.filteredDoctorList.filter((doctor:Doctor)=>doctor.fees>=100 && doctor.fees<200);

  }
  else if(this.feesFilter===4){
    this.filteredDoctorList=this.filteredDoctorList.filter((doctor:Doctor)=>doctor.fees>=200 && doctor.fees<300);

  }
  else{
    this.filteredDoctorList=this.filteredDoctorList.filter((doctor:Doctor)=>doctor.fees>=300);

  }

}


onGenderChange(GenderID:number,event:any){
  if(event.target.checked){
    this.genderFilter.push(GenderID)
  }
  else{
    this.genderFilter= this.genderFilter.filter((num:number)=>num!=GenderID)
  }
  this.Doctorfilter() 


}

onFeeChange(fee:number,event:any){
  if(event.target.checked){
    this.feesFilter=fee
  }
 
  this.Doctorfilter() 


}
onSubSpecialityChange(subspecialityId:number,event:any){
  if(event.target.checked){
    this.subspecialityFilter.push(subspecialityId)
  }
  else{
    this.subspecialityFilter= this.subspecialityFilter.filter((num:number)=>num!=subspecialityId)
  }
  this.Doctorfilter() 
  
 

}

  
  fileterDoctorWhenLoadingPage(){
    if(this.specialityId>0){
      this.filterDoctorBySpeciality()
    }
    if(this.cityName !="Egypt"){
      if(this.regionName!="All"){
         this.filterDoctorByFullAddress();
      }
      else{
             this.filterDoctorBycity()
      }
    }

    if(this.Doctorname!="All"){
      this.filterDoctorByName()
    }

  }
  filterDoctorBySpeciality(){
    this.DoctorList=this.DoctorList.filter((doctor:Doctor)=>{
      return doctor.specialityID==this.specialityId
    });
  }

  filterDoctorBycity(){
    this.DoctorList=this.DoctorList.filter((doctor:Doctor)=>{
      return doctor.addressDto.city===this.cityName
    });
  }

  filterDoctorByFullAddress(){
    this.DoctorList=this.DoctorList.filter((doctor:Doctor)=>{
      return doctor.addressDto.city===this.cityName && doctor.addressDto.region===this.regionName
    });
  }

  filterDoctorByName(){
    this.DoctorList=this.DoctorList.filter((doctor:Doctor)=>{
      return doctor.name.indexOf(this.Doctorname) > -1
    });
  }
  changeSorting(selectObject:any) {
    this.Doctorfilter() 
    this.sortDoctor(selectObject.target.value)
  }

  sortDoctor(sortby:number){
    if(sortby==1){
      this.sortDoctorByRatingDesc()
    }

    else if(sortby==2){
      this.sortDoctorByFeesAsec()
    }
    
    else if(sortby==3){
      this.sortDoctorByFeesDesc()
     }
     else{
      this.sortDoctorByLessWaitingTime()
     }


    


  }
  sortDoctorByRatingDesc(){
    this.filteredDoctorList.sort((firstDoctor, secondDoctor) => {
      return secondDoctor.rating - firstDoctor.rating;
   });

  }
  sortDoctorByFeesAsec(){
    this.filteredDoctorList.sort((firstDoctor, secondDoctor) => {
      return firstDoctor.fees - secondDoctor.fees;
  });

  }
  sortDoctorByFeesDesc(){
    this.filteredDoctorList.sort((firstDoctor, secondDoctor) => {
      return secondDoctor.fees - firstDoctor.fees;
  });
    

  }
  sortDoctorByLessWaitingTime(){
    this.filteredDoctorList.sort((firstDoctor, secondDoctor) => {
      return firstDoctor.waitingTime - secondDoctor.waitingTime;
  });
  }
 

}
