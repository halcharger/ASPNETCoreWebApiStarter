import { NgModule } from '@angular/core';


import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DashboardComponent } from './dashboard.component';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule, JsonpModule, NgbModule.forRoot()],
    declarations: [DashboardComponent],
    exports: [DashboardComponent]
})
export class DashboardModule { }