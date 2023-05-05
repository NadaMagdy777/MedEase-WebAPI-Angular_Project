export interface IreserveAppointement {
    date: string,
    patientID: number,
    doctorID: number,
    hasInsurance: boolean,
    insurancesId: number,
    hasInvestigations: boolean,
    description: string,
    hasImage: boolean,
    image:string
}
