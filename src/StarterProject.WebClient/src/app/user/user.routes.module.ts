import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListComponent } from './user-list.component';

const userRoutes: Routes =    [
        { path: 'users', component: UserListComponent }
    ];

@NgModule({
    imports: [
        RouterModule.forChild(userRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class UserRoutingModule { }