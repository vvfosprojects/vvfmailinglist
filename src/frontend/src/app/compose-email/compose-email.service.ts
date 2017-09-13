import { Injectable } from '@angular/core';
import { MailingListsInfo } from './compose-email.model';

import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { environment } from "environments/environment";

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

const API_URL = environment.apiUrl;

@Injectable()
export class ComposeEmailService {
  constructor(
    private http: Http
  ) { }

  getMailingListsInfoObservable(): Observable<MailingListsInfo[]> {
    return this.http
      .get(API_URL + '/EmailManager')
      .map(response => {
        const arrayMailingList = response.json();
        return arrayMailingList.map(obj => {
          var mli = Object.create(MailingListsInfo.prototype);
          Object.assign(mli, obj);
          return mli;
        });
      })
      .catch(this.handleError);
  }

  private handleError(error: any) {
    let errMsg = 'Message: ' + error.message + ' Status: ' + error.status + ' - ' + error.statusText;
    return Observable.throw(errMsg);
  }
}
