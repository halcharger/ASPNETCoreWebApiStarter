import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class HttpClient {

    constructor(private http: Http, private router: Router) { }

    createAuthorizationHeader(headers: Headers) {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            headers.append('Authorization', 'bearer ' + currentUser.token);
            return;
        }

        this.router.navigate(['/login']);
    }

    get(url) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.get(url, {
            headers: headers
        })
            .map(this.extractData)
            .catch(this.handleError);
    }

    post(url, data) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.post(url, data, {
            headers: headers
        });
    }

    private extractData(res: Response) {
        //We could check headers here if need be.
        let data = res.json();
        return data || {};
    }

    private handleError(error: Response | any) {
        // In a real world app, we might use a remote logging infrastructure
        //.catch(this.handleError);

        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}