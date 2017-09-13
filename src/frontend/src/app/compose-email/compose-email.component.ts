import { Component, OnInit } from '@angular/core';
import { ComposeEmailService } from './compose-email.service';
import { MailingListsInfo } from './compose-email.model';

@Component({
  selector: 'compose-email',
  templateUrl: './compose-email.component.html',
  styleUrls: ['./compose-email.component.css']
})
export class ComposeEmailComponent implements OnInit {
  mailingListsInfo: MailingListsInfo[];
  errormsg;
  constructor(private CES:ComposeEmailService) { 
  }

  ngOnInit() {
    this.getMailingListsInfo();  
  }

 getMailingListsInfo() {
    this.CES.getMailingListsInfoObservable()
                .subscribe(               
                  response => this.mailingListsInfo = response,
                  error => this.errormsg = error
                );
  }

  Close() {
  }
}
