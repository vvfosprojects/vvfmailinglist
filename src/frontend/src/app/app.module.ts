import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { ComposeEmailComponent } from './compose-email/compose-email.component';
import { ComposeEmailService } from './compose-email/compose-email.service';
import { ComposeEmailFakeService } from "app/compose-email/compose-email-fake.service";

@NgModule({
  declarations: [
    AppComponent,
    ComposeEmailComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
  ],
  providers: [
    { provide: ComposeEmailService, useClass: ComposeEmailService }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
