import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DoctorService } from 'src/app/Services/Doctor/doctor.service';
import { Doctor } from 'src/app/SharedClassesAndTypes/Doctor/Doctor';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
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
selectedSorting:any=0
specialityId:number=0
cityName:string="Egypt"
regionID:number=0
Doctorname:string=""



constructor(private DoctorService:DoctorService,private router:Router ,private route:ActivatedRoute){
   
    
    
}
Doctorfilter(){
  this.filteredDoctorList=this.DoctorList
  if(this.genderFilter.length>0){
    this.filterDoctorByGender()

  }
  if(this.feesFilter>0){
    this.filterDoctorByFees()
  }
}

filterDoctorByGender(){
  this.filteredDoctorList=this.filteredDoctorList.filter((doctor:Doctor)=>{
    return this.genderFilter.includes(doctor.gender)
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

  ngOnInit(): void {
    this.DoctorService.GetAllDoctors().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.DoctorList=dataJson.data
        this.filteredDoctorList=this.DoctorList
       console.log(this.DoctorList)},
      error:error=>this.errorMessage=error
    })
    this.specialityId= this.route.snapshot.params['speciality']
    this.cityName=this.route.snapshot.params['city']
    this.regionID=this.route.snapshot.params['region']
    this.Doctorname=this.route.snapshot.params['name']
    console.log(this.specialityId)
    console.log(this.cityName)
    console.log(this.regionID)
    console.log(this.Doctorname)
    
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
