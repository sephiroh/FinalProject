import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { LoginService } from './app-login.service';
import { UserToken } from './userToken.model';
import { LoggedUserService } from './../common/logged-user.service';

@Component({
    selector: 'app-Login',
    templateUrl: './app/app-login/app-login.component.html',
    providers: [LoginService]
})

export class LoginComponent implements OnInit {
    dataLoading = false;
    //returnUrl: string;
    hasErrors = false;
    private errorMessage: string;
    private vm: any = {};

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private loginService: LoginService,
        private loggedUserService: LoggedUserService) { }

    ngOnInit() {
        this.loginService.logout();
    }

    login() {
        this.dataLoading = true;
        this.hasErrors = false;
        this.loginService.login(this.vm.username, this.vm.password)
            .subscribe(
            data => {
                // broadcast to trigger change in displayed username in header component
                this.loggedUserService.broadcastLoggedUsername(data.firstname + " " + data.lastname);
                this.loggedUserService.redirectAfterLoggedIn(data);
            },
            error => {
                this.errorMessage = error.json();
                this.hasErrors = true;
                this.dataLoading = false;
            });
    }
}
