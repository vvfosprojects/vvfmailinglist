import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from "@angular/forms";

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
    FormsModule
  ],
  providers: [
    { provide: ComposeEmailService, useClass: ComposeEmailService }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
