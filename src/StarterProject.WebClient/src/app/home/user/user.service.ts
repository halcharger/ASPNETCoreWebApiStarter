// Observable Version
import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient  } from '../../common/http-client';
import { Headers, RequestOptions } from '@angular/http';

import { User } from "./user";
import { Observable } from 'rxjs/Observable';

@Injectable()
export class UserService {

    constructor(private http: HttpClient) { }

    getUsers(): Observable<User[]> {
        return this.http.get('api/users');
    }
}