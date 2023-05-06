export class Schedule{
    constructor(
        public doctorId:number,
        public isWorking:boolean,
        public weekDay:any,
        public startTime:any,
        public endTime:any,
        public timeInterval:number,
    ){}
}
export class EditSchedule{
    constructor(
        public id:number,
        public doctorId:number,
        public isWorking:boolean,
        public weekDay:any,
        public startTime:any,
        public endTime:any,
        public timeInterval:number,
    ){}
}

