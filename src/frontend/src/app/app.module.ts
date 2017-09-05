import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ComposeEmailComponent } from './compose-email/compose-email.component';
import { ListNamesComponent } from './list-names/list-names.component';

@NgModule({
  declarations: [
    AppComponent,
    ComposeEmailComponent,
    ListNamesComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
