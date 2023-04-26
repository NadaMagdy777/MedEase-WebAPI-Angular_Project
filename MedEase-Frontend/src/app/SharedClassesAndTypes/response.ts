import { IAddress } from "./iaddress";

export interface IResponse {
    statusCode: number;
    message: string;
    success: boolean; 
    data: IAddress[];
}
