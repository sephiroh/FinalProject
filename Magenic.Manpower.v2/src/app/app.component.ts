import { Component } from '@angular/core';
import { LoggedUserService } from './common/logged-user.service';

@Component({
    selector: 'manpower-app',
    templateUrl: './app/app.component.html',

    // This style is temporary solution for the toaster to work properly
    styles: [`
    .navbar{
        z-index: 999 !important; 
    }`
    ]
})

export class AppComponent {
    name = 'ManpowerApp';

    constructor(private loggedUserSvc: LoggedUserService)
    {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser != undefined) {
            this.loggedUserSvc.broadcastLoggedUsername(currentUser.firstname + " " + currentUser.lastname);
        }
    }
}
