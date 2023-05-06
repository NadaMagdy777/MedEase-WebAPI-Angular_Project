export interface IQuestionDto {
  id: number;
  title: string;
  description: string;
  answer: string;
  docName: string;
  dateCreated: Date;
  isAnswered: boolean;
  specialityId: number;
  patientId: number;
  doctorId: number;
}
