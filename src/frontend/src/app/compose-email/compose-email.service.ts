import { Injectable } from '@angular/core';
import { MailingListsInfo, SendMail } from './compose-email.model';

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

  
  getBackendCheckObservable(): Observable<string> {
    return this.http
      .get(API_URL + '/BackendCheck')
      .map(response => {
        return response.statusText;
      })
      .catch(this.handleError);
  }

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

  sendMailObservable(obj: SendMail): Observable<SendMail> {
    return this.http
      .post(API_URL + '/EmailManager', obj)
      .map(response => {
        const  appo_sm = response.json();
        var sm = Object.create(SendMail.prototype);
        Object.assign(sm, appo_sm);  
        return sm;
      })
      .catch(this.handleError);
  }

  private handleError(error: any) {   
    let errMsg = 'Message: ' + error.message + ' Status: ' + error.status + ' - ' + error.statusText;
    return Observable.throw(errMsg);
  }
}
