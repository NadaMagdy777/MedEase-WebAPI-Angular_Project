import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { DoctorService } from 'src/app/services/doctor/doctor.service';

@Component({
  selector: 'app-appointements',
  templateUrl: './appointements.component.html',
  styleUrls: ['./appointements.component.css']
})
export class AppointementsComponent {
  @Input() selectedDoctor!:number;
  showMore=false;
  times: any[] = [];
  reserved:any[]=[];
  constructor(private doctorService: DoctorService,private router:Router) {
    
    
  }
  
  ngOnInit(): void {    
    this.doctorService.GetDoctorAppointementAndPattern(this.selectedDoctor).subscribe({
      next: response => {
        console.log(response);

        response.data.forEach((responceday: any,index=0) => {
                
          let day = new Date(responceday.weekDay);
          
          //reserved appointements
          this.reserved.push({ key: day.toLocaleDateString(), value: [] });
          if(responceday.reservedAppointmanet != ''){
            responceday.reservedAppointmanet.forEach((reservedtime:any) => {
              let apidate = new Date(reservedtime);
              this.reserved[index].value.push(apidate.toLocaleTimeString());
            });
          }

          //free time
          let starttime = new Date(`${day.getFullYear()},${day.getMonth() + 1},${day.getDate()},${responceday.startTime}`)
          let endtime = new Date(`${day.getFullYear()},${day.getMonth() + 1},${day.getDate()},${responceday.endTime}`)

          this.times.push({ key: day.toLocaleDateString(), value: [] });
          this.times[index].value.push(starttime.toLocaleTimeString());

          while (starttime < endtime) {
            let s = starttime.getTime();
            let nexttime = s + (responceday.pattern * 60 * 1000);
            starttime = new Date(nexttime);
            this.times[index].value.push(starttime.toLocaleTimeString());
          }

          index++;
          
        });
        console.log(this.reserved);
        console.log(this.times);
      },

      error: error => console.log(error)

    })
  }
  ShowMore(){
    this.showMore=true;
  }
  navigate(d:any,t:any){
    console.log(d,t);
    
    this.router.navigate(["reserve-appointement"],{ state:{date:d, time:t,doctor:this.selectedDoctor}})
    // this.router.navigate(["reserve-appointement"],{ state:{date:d, time:t}})
  }
}
