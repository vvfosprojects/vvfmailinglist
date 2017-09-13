import { Injectable } from '@angular/core';
import { Observable } from "rxjs/Rx";
import { MailingListsInfo } from "app/compose-email/compose-email.model";

@Injectable()
export class ComposeEmailFakeService {

  constructor() { }

  public getMailingListsInfoObservable(): Observable<MailingListsInfo[]> {
    return Observable.of([
      new MailingListsInfo('id1', 'Mailing List 1'),
      new MailingListsInfo("id2", "Mailing List 2"),
      new MailingListsInfo("id3", "Mailing List 3"),
      new MailingListsInfo("id4", "Mailing List 4"),
    ]);
  }

}
