export class Review{
    constructor(
     public comment:string,
      public doctorRate: number,
      public clinicRate: number,
      public waitingTimeinMins: number,
      public examinationID: number,
      public patientName:string
    ){}
}