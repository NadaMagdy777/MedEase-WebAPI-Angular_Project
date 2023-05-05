import { IUserRegisterDto } from '../patient/iuser-register-dto';

export interface IDoctorRegisterDto extends IUserRegisterDto {
  allowVisa: boolean;
  fees: number;
  faculty: string;
  specialityID: string;
  subSpecialities?: number[];
  insurances?: number[];
  licenseImg: string;
  profilePicture: string;
}
