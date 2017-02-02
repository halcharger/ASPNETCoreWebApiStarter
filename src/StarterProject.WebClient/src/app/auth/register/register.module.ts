import { NgModule } from '@angular/core';


import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { RegisterRoutingModule } from './register.route.module';
import { RegisterComponent } from './register.component';


@NgModule({
    imports: [RegisterRoutingModule, BrowserModule, FormsModule, HttpModule, JsonpModule, NgbModule.forRoot()],
    declarations: [RegisterComponent],
    exports: [RegisterComponent],
    providers: []
})
export class RegisterModule { }