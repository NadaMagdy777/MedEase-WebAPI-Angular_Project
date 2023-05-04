import { HttpClient } from '@angular/common/http';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { Injectable } from '@angular/core';
import { IPatientQuestionDto } from 'src/app/SharedClassesAndTypes/patient/ipatient-question-dto';
import { IApiResponse } from 'src/app/sharedClassesAndTypes/iapi-response';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class QuestionService {
  constructor(
    private _userAuthService: UserAuthService,
    private _httpClient: HttpClient
  ) {}

  baseUrl: string = environment.apiUrl + '/Question/';

  askQuestion(dto: any): Observable<IApiResponse> {
    const question: IPatientQuestionDto = this.getUserQuestion(dto);
    console.log(question);

    return this._httpClient.post<IApiResponse>(
      this.baseUrl + 'Patient/Ask',
      question
    );
  }

  private getUserQuestion(dto: any): IPatientQuestionDto {
    const { title, description, specialityId } = dto;
    const patientId = this._userAuthService.getLoggedUserId;
    const question: IPatientQuestionDto = {
      title,
      description,
      specialityId: Number.parseInt(specialityId),
      patientId: Number.parseInt(patientId),
    };
    return question;
  }
}
