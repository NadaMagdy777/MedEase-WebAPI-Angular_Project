export class Doctor{
    constructor(
        public id:number,
        public fees:number,
        public faculty:string,
        public name:string,
        public phoneNumber:string,
        public gender:any,
        public age:number,
        public building:number,
        public street :string,
        public addressDto:any,
        public doctorSubspiciality:any,
        public specialityName:string,
        public doctorcertificates:any,
        public doctorcerInsurance:any
    ){}
}