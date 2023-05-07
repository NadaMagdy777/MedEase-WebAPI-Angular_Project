import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'birthdateToAge'
})
export class BirthdateToAgePipe implements PipeTransform {

  transform(birthDate:any): any {
    var dataNow = new Date();
    birthDate=new Date(birthDate)
    var age = dataNow.getFullYear() - birthDate.getFullYear();

            if (dataNow.getMonth() < birthDate.getMonth() || (dataNow.getMonth() == birthDate.getMonth() && dataNow.getDay() < birthDate.getDay()))
                age--;

            return age;
    
  }

}
