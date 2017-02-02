// Observable Version
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../common/authentication.service';

@Component({
    moduleId: module.id,
    selector: 'home',
    templateUrl: 'home.component.html'
})
export class HomeComponent {
    constructor(private authenticationService: AuthenticationService, private router: Router) { }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}
