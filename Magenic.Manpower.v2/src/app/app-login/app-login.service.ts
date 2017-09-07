import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Settings } from '../common/global-settings.service';
import { UserToken } from './userToken.model';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class LoginService {
    public token: string;

    constructor(private http: Http, private settings: Settings) {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser;
    }

    login(username: string, password: string): Observable<UserToken> {
        let credential = {
            Username: username,
            Password: password
        };

        const url = this.settings.apiURL + '/login';

        let headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.post(url, JSON.stringify(credential), headers)
            .map((response: Response) => {
                let result = response.json() && response.json().responseData;
                if (result && response.json().success) {
                    if (result.permissions.length === 0) {
                        return { Errors: 'No permissions found.' };
                    } else {
                        localStorage.setItem('currentUser', JSON.stringify(result));
                        return result;
                    }
                } else {
                    return { Errors: response.json().errors[0] };
                }
            }).catch((error: any) => Observable.throw(error.json().error || 'Server error'));
    }

    logout(): void {
        this.token = null;
        localStorage.removeItem('currentUser');
    }
}
