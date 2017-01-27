import { NgModule } from '@angular/core';


import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeRoutingModule } from './home.routes.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { UserModule } from './user/user.module';
import { AuthGuard } from '../common/auth.guard';


import { HomeComponent } from './home.component';

@NgModule({
    imports: [UserModule, DashboardModule, HomeRoutingModule, BrowserModule, FormsModule, HttpModule, JsonpModule, NgbModule.forRoot()],
    declarations: [HomeComponent],
    exports: [HomeComponent],
    providers: [AuthGuard]
})
export class HomeModule { }