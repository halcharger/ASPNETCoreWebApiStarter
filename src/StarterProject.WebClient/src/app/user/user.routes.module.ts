﻿import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListComponent } from './user-list.component';


        { path: 'users', component: UserListComponent }
    ];

@NgModule({
    imports: [
        RouterModule.forRoot(userRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class UserRoutingModule { }