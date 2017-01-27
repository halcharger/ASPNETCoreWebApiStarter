// Observable Version
import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient  } from '../../common/http-client';
import { Headers, RequestOptions } from '@angular/http';

import { User } from "./user";
import { Observable } from 'rxjs/Observable';

@Injectable()
export class UserService {
    private usersUrl = 'http://localhost:65018/api/users?t=' + new Date().getTime() / 1000;  // URL to web API

    constructor(private http: HttpClient) { }

    getUsers(): Observable<User[]> {
        return this.http.get(this.usersUrl)
            .map(this.extractData)
            .catch(this.handleError);
    }

    private extractData(res: Response) {
        let data = res.json();
        return data || {};
    }

    private handleError(error: Response | any) {
        // In a real world app, we might use a remote logging infrastructure
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