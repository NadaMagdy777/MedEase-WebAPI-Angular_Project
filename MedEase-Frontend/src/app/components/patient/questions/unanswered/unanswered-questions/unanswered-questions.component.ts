import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IQuestionDto } from 'src/app/SharedClassesAndTypes/questions/IQuestionDto';
import { QuestionService } from 'src/app/Services/question/question.service';

@Component({
  selector: 'app-unanswered-questions',
  templateUrl: './unanswered-questions.component.html',
  styleUrls: ['./unanswered-questions.component.css']
})
export class UnansweredQuestionsComponent implements OnInit, OnDestroy {
  constructor(private _questionService: QuestionService) {}

  allQuestions: IQuestionDto[] = [];
  allSubscription: Subscription[] = [];
  serverErrorMsg: string = '';

  ngOnInit(): void {
    this._questionService.getPatientQuestions(false).subscribe((res) => {
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