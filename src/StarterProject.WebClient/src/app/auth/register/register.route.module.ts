﻿import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register.component';


        {
            path: 'register',
            component: RegisterComponent
        }
    ];

@NgModule({
    imports: [
        RouterModule.forChild(registerRoute)
    ],
    exports: [
        RouterModule
    ]
})
export class RegisterRoutingModule { }