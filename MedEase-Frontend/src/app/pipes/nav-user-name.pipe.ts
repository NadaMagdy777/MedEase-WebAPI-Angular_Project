import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'navUserName',
})
export class NavUserNamePipe implements PipeTransform {
  transform(fullName: string): string {
    let name = fullName.split(' ')[0];
    return 'Hi ' + name + ' !';
  }
}
