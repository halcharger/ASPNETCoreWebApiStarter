import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';

import { requestOptionsProvider } from './default-request-options.service';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { UserListComponent } from './user/user-list.component';
import { DashboardComponent } from './dashboard/dashboard.component';


@NgModule({
    declarations: [
        AppComponent,
        UserListComponent,
        DashboardComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        JsonpModule,
        AppRoutingModule,
        NgbModule.forRoot()
    ],
    providers: [requestOptionsProvider],
    bootstrap: [AppComponent]
})
export class AppModule { }
