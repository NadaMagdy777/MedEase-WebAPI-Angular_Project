import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'minutesToTime'
})
export class MinutesToTimePipe implements PipeTransform {

  transform(value: number): any {
    var hours = Math.floor(value / 60);  
      var minutes = value % 60;
    return hours + " h :" + minutes+" m";
  }

}
