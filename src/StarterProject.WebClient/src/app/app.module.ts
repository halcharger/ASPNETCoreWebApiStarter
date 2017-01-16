import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';

import { requestOptionsProvider } from './default-request-options.service';

import { AppComponent } from './app.component';

import { UserListComponent } from './user/user-list.component';


@NgModule({
    declarations: [
        AppComponent,
        UserListComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        JsonpModule,
    ],
    providers: [requestOptionsProvider],
    bootstrap: [AppComponent]
})
export class AppModule { }
