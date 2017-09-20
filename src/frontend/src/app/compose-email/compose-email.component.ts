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
  errormsg: string;
  constructor(private CES: ComposeEmailService) {
  }

  ngOnInit() {
    this.getMailingListsInfo();
    this.frm_sendMail = new SendMail();
    this.frm_sendMail.idListeDestinatarie = [];
  }

  getMailingListsInfo() {
    this.CES.getMailingListsInfoObservable()
      .subscribe(
      response => this.mailingListsInfo = response,
      error => this.errormsg = error
      );
  }

  sendMail(sendMail: SendMail) {
    this.CES.sendMailObservable(sendMail)
      .subscribe(
      response => {
        this.post_sendMail = response;
        this.frm_sendMail.Oggetto = null;
        this.frm_sendMail.Corpo = null;
      },
      error => this.errormsg = error
      );
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
    return !!this.frm_sendMail.Oggetto && !!this.frm_sendMail.Corpo;
  }
}
