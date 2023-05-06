import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DoctorService } from 'src/app/Services/Doctor/doctor.service';
import { Doctor } from 'src/app/SharedClassesAndTypes/Doctor/Doctor';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ISubSpecialty } from 'src/app/sharedClassesAndTypes/Doctor/isub-specialty';
import { SpecialtiesService } from 'src/app/services/specialities/specialities.service';
import { ImageService } from 'src/app/services/image.service';
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
subspecialityFilter:number[]=[]
selectedSorting:any=0
specialityId:number=0
cityName:string="Egypt"
regionName:string="All"
Doctorname:string="All"
subspiciality:ISubSpecialty[]=[]

constructor(private DoctorService:DoctorService,private router:Router ,private route:ActivatedRoute,private specialityService: SpecialtiesService,private _imageService:ImageService){
   
    
    
}
ngOnInit(): void {
  this.DoctorService.GetAllDoctors().subscribe({
    next:data=>{
      let dataJson = JSON.parse(JSON.stringify(data))
      this.DoctorList=dataJson.data
       console.log(this.DoctorList)
      this.specialityId= this.route.snapshot.params['speciality']
      this.cityName=this.route.snapshot.params['city']
      this.regionName=this.route.snapshot.params['region']
      this.Doctorname=this.route.snapshot.params['name']
      console.log(this.specialityId)
      console.log(this.cityName)
      console.log(this.regionName)
      console.log(this.Doctorname)
      this.specialityService.GetSubspicilityBySpecialityId(this.specialityId).subscribe({
        next:data=>{
          let dataJson = JSON.parse(JSON.stringify(data))
           this.subspiciality=dataJson.data
           console.log(this.subspiciality)


        },
        error:error=>this.errorMessage=error
      })
      this.fileterDoctorWhenLoadingPage()
      this.filteredDoctorList=this.DoctorList
      this.filteredDoctorList.forEach((doctor:Doctor)=>{
        doctor.profilePicture=this._imageService.base64ArrayToImage(doctor.profilePicture)
        console.log(doctor.profilePicture)
      })

      
      

      console.log(this.filteredDoctorList)

    },
    error:error=>this.errorMessage=error
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
  console.log("filtered list")
  console.log(this.filteredDoctorList)
  
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
      console.log("filter by speciality")
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
