import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { UserToken } from './../app-login/userToken.model';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable()
export class LoggedUserService {
    // observable string sources
    private announceUsername = new BehaviorSubject<string>("");
    //returnUrl: string;

    constructor(
        private router: Router,
        private route: ActivatedRoute) { }

    broadcastLoggedUsername(username: string) {
        this.announceUsername.next(username);
    }

    get loggedUsername$() {
        return this.announceUsername.asObservable();
    }

    redirectAfterLoggedIn(data: UserToken) {
        let permissions = data.permissions;
        var returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

        permissions.forEach(function (permission) {
            if (permission == 'Tag Applicants') {
                returnUrl = '/hrdashboard';
            }
            else if (permission == 'Request Manpower' || permission == 'Cancel Request') {
                returnUrl = '/cmdashboard';
            }
            else {
                returnUrl = '/projectManagement';
            }
        });

        this.router.navigate([returnUrl]);
    }
}