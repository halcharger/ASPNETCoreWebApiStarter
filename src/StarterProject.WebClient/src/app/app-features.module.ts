import { NgModule } from '@angular/core';

import { HomeModule } from './home/home.module';
import { AuthModule } from './auth/auth.module';

@NgModule({
    imports: [],
    exports: [HomeModule, AuthModule]
})
export class AppFeaturesModule { }