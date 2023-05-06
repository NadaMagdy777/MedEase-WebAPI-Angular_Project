import { IQuestionDto } from 'src/app/sharedClassesAndTypes/questions/IQuestionDto';
import { QuestionService } from './../../../../../services/question/question.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-answered-questions',
  templateUrl: './answered-questions.component.html',
  styleUrls: ['./answered-questions.component.css'],
})
export class AnsweredQuestionsComponent implements OnInit, OnDestroy {
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
