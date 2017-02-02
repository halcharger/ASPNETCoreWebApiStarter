import { NgModule } from '@angular/core';
import { Route } from '@angular/router';
import { UserListComponent } from './user-list.component';

export const userRoutes: Route = { path: 'users', component: UserListComponent };