import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'list-names',
  templateUrl: './list-names.component.html',
  styleUrls: ['./list-names.component.css']
})
export class ListNamesComponent implements OnInit {
  @Input() visible : boolean;

  constructor() { }

  ngOnInit() {
  }

  Chiudi() {
    this.visible = false;
  }
}
