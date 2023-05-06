import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { QuestionService } from 'src/app/services/question/question.service';
import { IQuestionDto } from 'src/app/sharedClassesAndTypes/questions/IQuestionDto';

@Component({
  selector: 'app-doctor-un-answerd',
  templateUrl: './doctor-un-answerd.component.html',
  styleUrls: ['./doctor-un-answerd.component.css']
})
export class DoctorUnAnswerdComponent implements OnInit, OnDestroy {
  constructor(private _questionService: QuestionService) {}

  allQuestions: IQuestionDto[] = [];
  allSubscription: Subscription[] = [];
  serverErrorMsg: string = '';

  ngOnInit(): void {
    this._questionService.getDoctorUnAnswerdQuestions().subscribe((res) => {
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