import { NgModule } from '@angular/core';
import { AuthenticationService } from '../common/authentication.service';


import { LoginModule } from './login/login.module';
import { RegisterModule } from './register/register.module';


@NgModule({
    imports: [],
    exports: [LoginModule, RegisterModule],
    providers: [AuthenticationService]
})
export class AuthModule { }