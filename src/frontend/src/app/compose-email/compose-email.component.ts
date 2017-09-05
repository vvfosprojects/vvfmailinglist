import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'compose-email',
  templateUrl: './compose-email.component.html',
  styleUrls: ['./compose-email.component.css']
})
export class ComposeEmailComponent implements OnInit {
 showNames : boolean = false;
  @Input() visible : boolean;
  
  constructor() { 
  }

  ngOnInit() {
  }

  ShowNames() {
    this.showNames = true;
  } 
}
