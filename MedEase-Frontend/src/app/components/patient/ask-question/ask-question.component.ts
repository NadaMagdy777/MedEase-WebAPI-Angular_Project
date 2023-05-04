import { Router } from '@angular/router';
import { QuestionService } from './../../../services/question/question.service';
import { SpecialtiesService } from './../../../services/specialities/specialities.service';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-ask-question',
  templateUrl: './ask-question.component.html',
  styleUrls: ['./ask-question.component.css'],
})
export class AskQuestionComponent {
  constructor(
    private _specialtiesService: SpecialtiesService,
    private _questionService: QuestionService,
    private _router: Router
  ) {}

  specialties$ = this._specialtiesService.getSpecialties();
  isLoading: boolean = false;

  questionForm: FormGroup = new FormGroup({
    title: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    specialityId: new FormControl('', Validators.required),
  });

  onSubmit() {
    console.log(this.questionForm.value);
    this.isLoading = true;

    this._questionService
      .askQuestion(this.questionForm.value)
      .subscribe((res) => {
        if (res.success) {
          this.isLoading = false;
          this.questionForm.reset();
          this._router.navigate(['/account/questions']);
        }
      });
  }
}
