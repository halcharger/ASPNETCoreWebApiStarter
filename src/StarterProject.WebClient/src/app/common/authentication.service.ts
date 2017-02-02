import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions, URLSearchParams  } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'
import { User } from "../home/user/user";
import { environment } from '../../environments/environment';


@Injectable()
export class AuthenticationService {

    constructor(private http: Http) {

    }

    login(username: string, password: string): Observable<boolean> {
        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let data = new URLSearchParams();
        data.append('username', username);
        data.append('password', password);
        return this.http.post(environment.serverBaseUrl + 'api/token', data, new RequestOptions({ headers: headers }))
            .map((response: Response) => {
                // login successful if there's a jwt token in the response
                let token = response.json() && response.json().access_token;
                if (token) {
                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));

                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            });
    }

    register(user: User) {
        return this.http.post(environment.serverBaseUrl + 'api/users/register', user)
            .map((response: Response) => {
                let data = response.json();
                return data;
            });
    }

    logout(): void {
        // clear token remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}