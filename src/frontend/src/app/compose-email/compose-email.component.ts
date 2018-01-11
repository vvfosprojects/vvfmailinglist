import { Component, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';

import { ComposeEmailService } from './compose-email.service';
import { MailingListsInfo, SendMail } from './compose-email.model';

@Component({
  selector: 'compose-email',
  templateUrl: './compose-email.component.html',
  styleUrls: ['./compose-email.component.css']
})
export class ComposeEmailComponent implements OnInit {
  mailingListsInfo: MailingListsInfo[];
  sendMailValue: SendMail;
  sendMailResponse: SendMail;
  errorMsg: string;
  userMsg: string;
  inFaseDiInvio: boolean = false;
  mailingListIds = { };

  constructor(private CES: ComposeEmailService) {
  }

  ngOnInit() {
    this.getBackendCheck();
    this.sendMailValue = new SendMail();
  }

  getBackendCheck() {
    this.CES.getBackendCheckObservable()
      .subscribe(
        response => {
          if (response == "OK") {
            this.getMailingListsInfo();
          }
          else {
            this.errorMsg = "Il backend è irraggiungibile. Non sarà possibile inviare email.";
          }   
        },
        error => this.errorMsg = "Il backend è irraggiungibile. Non sarà possibile inviare email."
      );
  }

  getMailingListsInfo() {
    this.CES.getMailingListsInfoObservable()
      .subscribe(
      response => this.mailingListsInfo = response,
      error => this.errorMsg = error
      );
  }

  private resetMessaggi() {
    this.userMsg = null;
    this.errorMsg = null;
  }

  private pulisciForm() {
    this.sendMailValue.Oggetto = null;
    this.sendMailValue.Corpo = null;
  }

  sendMail(sendMail: SendMail) {
    this.resetMessaggi();
    this.inFaseDiInvio = true;
    this.CES.sendMailObservable(sendMail)
      .subscribe(
      response => {
        this.sendMailResponse = response;
        this.pulisciForm();
        this.userMsg = "Messaggio inviato con successo";
      },
      error => {
        this.errorMsg = "Messaggio non inviato. " + error;
        this.inFaseDiInvio = false;
      },
      () => {
        this.inFaseDiInvio = false;
      });
  }

  Send() {
    this.sendMail(this.sendMailValue);
  }

  public isValidForm(): boolean {
    if (this.inFaseDiInvio)
      return false;

    if (!this.sendMailValue.Oggetto)
      return false;

    if (!this.sendMailValue.Corpo)
      return false;

    if (this.idListeSelezionate().length == 0)
      return false;

    this.sendMailValue.idListeDestinatarie = this.idListeSelezionate(); 
    return true;
  }

  private idListeSelezionate(): string[] {
    let chiavi = Object.keys(this.mailingListIds);
    let idSelezionati = chiavi
      .filter(c => !!this.mailingListIds[c]);
    return idSelezionati;
  }
}
