import { Injectable } from '@angular/core';
import { MailingListsInfo } from './compose-email.model';

import { Http, Response, RequestOptionsArgs, Headers, RequestMethod, Jsonp } from '@angular/http';
import { Observable } from 'rxjs/Rx';

@Injectable()
export class ComposeEmailService {
  private apiStringUrl: string = '//localhost:22537';
  mailingListsInfo: MailingListsInfo[];
   
  constructor(
    //private http: Http 
    private json : Jsonp
  ) { }

  getMailingListsInfo() :  MailingListsInfo[] {
    return this.mailingListsInfo;
  }

  getMailingListsInfoObservable() :  Observable<MailingListsInfo[]> {
    let options : RequestOptionsArgs = {
                      url: this.apiStringUrl,
                      method: RequestMethod.Get,
                      search: null,
                      headers: this.getHeaders(),  
                      body: null };

    let mymailingListsInfo = 
                    //this.http
                    //.map(mapMailingListsInfos)
                    //.get(this.apiStringUrl + '/api/emailManager', options)
                    this.json
                    .get('http:' + this.apiStringUrl + '/api/emailManager')
                    .map(response => <MailingListsInfo[]> response.json())
                    .catch(this.handleErrorObs);
    return mymailingListsInfo;                    
  }
  
  /*getMailingListsInfoObservable() :  MailingListsInfo[] {
    this.mailingListsInfo = [
      {'id':'L1','nome':'L1'} ,
      {'id':'L2','nome':'L2'}
    ]; 
    return this.mailingListsInfo; 
  }*/
  private getHeaders() {
      let myHeaders = new Headers();
      //myHeaders.append('Content-Type', 'application/json');
     // myHeaders.append('X-Requested-With', 'XMLHttpRequest');
     // myHeaders.append('x-access-token' , 'sometoken');
      //myHeaders.append('Accept', 'application/json');
      //myHeaders.append('Access-Control-Allow-Headers', 'Content-Type');
      //myHeaders.append('Access-Control-Allow-Methods', 'GET, POST, PUT');
      myHeaders.append('Access-Control-Allow-Origin','*');      
      return myHeaders
  }    

  private handleErrorObs(error: any) {
    let errMsg = //(error.message) ? error.message :
                 //   error.status ? `${error.status} - ${error.statusText}` : 
                        'Message: ' + error.message + ' Status: ' + error.status + ' - ' + error.statusText;            
    return Observable.throw(errMsg);    
  }
}

function mapMailingListsInfos(response: Response): MailingListsInfo[] {      
  console.log('mapMailingListsInfos -> Response [' + response + ']' );
  //return response.json().results.map(toMailingListsInfo);  
  return response.json().map(toMailingListsInfo);
}

function toMailingListsInfo(r: any): MailingListsInfo{      
  let myMLI = <MailingListsInfo>({
    id : r.id,
    nome: r.nome
  });

  console.log('toMailingListsInfo -> r [' + r + ']' );
  return myMLI;
}

  