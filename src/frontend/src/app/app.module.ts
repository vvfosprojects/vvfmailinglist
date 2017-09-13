import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule, JsonpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { ComposeEmailComponent } from './compose-email/compose-email.component';
import { ComposeEmailService } from './compose-email/compose-email.service';

@NgModule({
  declarations: [
    AppComponent,
    ComposeEmailComponent
  ],
  imports: [
    BrowserModule,
    HttpModule, 
    JsonpModule
  ],
  providers: [
    ComposeEmailService
  ],
  bootstrap: [ 
    AppComponent 
  ]
})
export class AppModule { }
