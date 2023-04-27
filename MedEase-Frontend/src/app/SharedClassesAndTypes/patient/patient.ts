export class Patient{
    constructor(
        public firstName:string,
        public lastName:string,
        public phoneNumber:string,
        public building:number,
        public street:string,
        public region :string,
        public city :string,
        public birthDate:any,
        public email:string,
        public insuranceName:string,
        public history:any
    ){}
}