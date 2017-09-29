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
  frm_sendMail: SendMail;
  post_sendMail: SendMail;
  errorMsg: string;
  userMsg: string;
  inFaseDiInvio: boolean = false;
  mailingListIds = { "id1": true };

  constructor(private CES: ComposeEmailService) {
  }

  ngOnInit() {
    this.getMailingListsInfo();
    this.frm_sendMail = new SendMail();
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
    this.frm_sendMail.Oggetto = null;
    this.frm_sendMail.Corpo = null;
  }

  sendMail(sendMail: SendMail) {
    this.resetMessaggi();
    this.inFaseDiInvio = true;
    this.CES.sendMailObservable(sendMail)
      .subscribe(
      response => {
        this.post_sendMail = response;
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

  updateSelectidListeDestinatarie(event) {
    if (event.target.checked) {
      if (!this.frm_sendMail.idListeDestinatarie.find(x => x == event.target.value)) {
        this.frm_sendMail.idListeDestinatarie.push(event.target.value);
      }
    }
    else {
      if (this.frm_sendMail.idListeDestinatarie.find(x => x == event.target.value)) {
        this.frm_sendMail.idListeDestinatarie.slice(event.target.value);
      }
    }
  }

  Send() {
    this.sendMail(this.frm_sendMail);
    console.log("post -> ", this.frm_sendMail);
  }

  private isValidForm(): boolean {
    if (this.inFaseDiInvio)
      return false;

    if (!this.frm_sendMail.Oggetto)
      return false;

    if (!this.frm_sendMail.Corpo)
      return false;

    if (this.idListeSelezionate().length == 0)
      return false;


    return true;
  }

  private idListeSelezionate(): string[] {
    let chiavi = Object.keys(this.mailingListIds);
    let idSelezionati = chiavi
      .filter(c => !!this.mailingListIds[c]);
    return idSelezionati;
  }
}
