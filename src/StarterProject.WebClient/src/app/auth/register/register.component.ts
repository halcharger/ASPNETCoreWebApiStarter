// Observable Version
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../common/authentication.service';
import { User } from '../../home/user/user';


@Component({
    moduleId: module.id,
    selector: 'register',
    templateUrl: 'register.component.html'
})
export class RegisterComponent {
    user: User = new User();
    loading = false;

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService) { }

    register() {
        this.loading = true;
        this.authenticationService.register(this.user)
            .subscribe(
            data => {
                //this.alertService.success('Registration successful', true);
                this.router.navigate(['/login']);
            },
            error => {
                //this.alertService.error(error);
                this.loading = false;
            });
    }
}
