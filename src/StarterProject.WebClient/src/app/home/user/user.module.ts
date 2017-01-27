import { NgModule } from '@angular/core';


import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { UserListComponent } from './user-list.component';
import { UserService } from './user.service';


@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule, JsonpModule, NgbModule.forRoot()],
    declarations: [UserListComponent],
    exports: [UserListComponent],
    providers: [UserService]
})
export class UserModule { }