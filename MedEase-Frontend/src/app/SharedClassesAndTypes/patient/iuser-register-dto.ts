import { Gender } from "../enums/gender";

export interface IUserRegisterDto {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    ssn: string;
    phoneNumber: string;
    gender: Gender;
    birthDate: Date;
    building: number;
    street: string;
    addressID: number;
}
