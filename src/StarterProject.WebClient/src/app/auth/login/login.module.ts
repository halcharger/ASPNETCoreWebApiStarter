import { NgModule } from '@angular/core';


import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { LoginRoutingModule } from './login.route.module';
import { LoginComponent } from './login.component';


@NgModule({
    imports: [LoginRoutingModule, BrowserModule, FormsModule, HttpModule, JsonpModule, NgbModule.forRoot()],
    declarations: [LoginComponent],
    exports: [LoginComponent],
    providers: []
})
export class LoginModule { }