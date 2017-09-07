import * as _ from 'underscore';

import { Component, OnDestroy } from '@angular/core';
import { AppRoutingModule } from './../app-router/app-routing.module';
import { LoginService } from './../app-login/app-login.service';
import { Router } from '@angular/router';
import { LoggedUserService } from './../common/logged-user.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'app-header',
    templateUrl: './app/app-header/app-header.component.html',
})

export class AppHeaderComponent implements OnDestroy {
    name = 'App Header';
    showCurrentUser = false;
    subscription: Subscription;
    private userName: string;

    constructor(private appRouting: AppRoutingModule,
        private loginService: LoginService,
        private router: Router,
        private loggedUserService: LoggedUserService) {
        // subscribed to the changes made during login
        this.subscription = this.loggedUserService.loggedUsername$.subscribe(
            item => {
                this.userName = item;
                this.showCurrentUser = this.userName !== "";
            }
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    get routes() {
        return _.filter(this.appRouting.Routes, function (_route: any) {
            if (_route.data && _route.data.isRoot) {
                let currentUser = JSON.parse(localStorage.getItem('currentUser'));
                if (currentUser && currentUser.id !== 0) {
                    if (Array.isArray(_route.data["permissions"])) {
                        return _.intersection(_route.data["permissions"], currentUser.permissions).length !== 0;
                    } else {
                        // will still show menu links, if route has no specified permissions
                        return true;
                    }
                }
            }
            return false;
        });
    }

    logoutUser() {
        this.loginService.logout();
        // clear logged username
        this.loggedUserService.broadcastLoggedUsername("");
        this.router.navigate(['/login']);
    }
}
