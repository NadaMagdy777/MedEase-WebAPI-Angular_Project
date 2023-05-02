import { IUserRegisterDto } from "../patient/iuser-register-dto";

export interface IDoctorRegisterDto extends IUserRegisterDto {
  allowVisa: boolean;
  fees: number;
  faculty: string;
  specialityIDTemp: string;
  subSpecialities?: number[];
  insurances?: number[];
  licenseImgForm: File;
  profilePictureForm: File;
}
