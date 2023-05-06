import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { QuestionService } from 'src/app/Services/question/question.service';
import { IQuestionDto } from 'src/app/SharedClassesAndTypes/questions/IQuestionDto';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';

@Component({
  selector: 'app-answer-question',
  templateUrl: './answer-question.component.html',
  styleUrls: ['./answer-question.component.css']
})
export class AnswerQuestionComponent implements OnInit {
  
  questionId:number=0;
  
  question:IQuestionDto={
    answer: "",
    id: 0,
    title: '',
    description: '',
    docName: '',
    dateCreated: new Date(),
    isAnswered: false,
    specialityId: 0,
    patientId: 0,
    doctorId: 0
  };

  isLoading: boolean = false;
  serverErrorMsg: string = '';
  doctorID:number=0;
  questionForm: FormGroup = new FormGroup({
    id:new FormControl(this.questionId),
    answer: new FormControl('', Validators.required),
    doctorId: new FormControl(this.doctorID),
  });

  constructor(private activatedRoute:ActivatedRoute,private userAuthService:UserAuthService, private _questionService: QuestionService,private _router: Router) {}

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((params:ParamMap)=>{
      this.questionId = <number> <unknown>params.get("id");
      this.questionForm.get('id')?.setValue(this.questionId);
      this.doctorID = <number> <unknown>this.userAuthService.getLoggedUserId;    
      this.questionForm.get('doctorId')?.setValue(this.doctorID);
      console.log(this.doctorID,this.questionId);
      
      
      this._questionService.getQuestion(this.questionId).subscribe((res) => {
        if (res.success) {
          this.question = res.data;
        } else {
          this.serverErrorMsg = res.message;
        }
      });
    })
  }
  onSubmit() { 
    this.isLoading = true;    
    this._questionService
      .answerQuestion(this.questionForm.value)
      .subscribe((res) => {
        if (res.success) {
          this.isLoading = false;
          this.questionForm.reset();
          this._router.navigate(['/doctor/questions/answered']);
        }
      });
  }
}
