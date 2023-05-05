export interface IreserveAppointement {
    date: Date,
    patientID: number,
    doctorID: number,
    hasInsurance: boolean,
    insurancesId: number,
    hasInvestigations: boolean,
    description: string,
    hasImage: boolean,
    // image: File
    image:string
}
