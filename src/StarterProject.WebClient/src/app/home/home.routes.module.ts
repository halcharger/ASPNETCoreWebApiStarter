import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home.component';
import { dashboardRoutes } from './dashboard/dashboard.routes.module';
import { userRoutes } from './user/user.routes.module';
import { AuthGuard } from '../common/auth.guard';

const homeRoutes: Routes =    [
        {
            path: 'home',
            component: HomeComponent,
            canActivate: [AuthGuard],
            children: [dashboardRoutes, userRoutes]
        }
    ];

@NgModule({
    imports: [
        RouterModule.forChild(homeRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class HomeRoutingModule {}