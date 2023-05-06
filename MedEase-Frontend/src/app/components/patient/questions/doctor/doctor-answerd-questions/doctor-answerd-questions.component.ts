import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IQuestionDto } from 'src/app/SharedClassesAndTypes/questions/IQuestionDto';
import { QuestionService } from 'src/app/Services/question/question.service';

@Component({
  selector: 'app-doctor-answerd-questions',
  templateUrl: './doctor-answerd-questions.component.html',
  styleUrls: ['./doctor-answerd-questions.component.css']
})
export class DoctorAnswerdQuestionsComponent implements OnInit, OnDestroy {
  constructor(private _questionService: QuestionService) {}

  allQuestions: IQuestionDto[] = [];
  allSubscription: Subscription[] = [];
  serverErrorMsg: string = '';

  ngOnInit(): void {
    this._questionService.getDoctorAnswerdQuestions().subscribe((res) => {
      if (res.success) {
        this.allQuestions = res.data;
      } else {
        this.serverErrorMsg = res.message;
      }
    });
  }

  ngOnDestroy(): void {
    this.allSubscription.forEach((subscription) => subscription.unsubscribe());
  }
}
