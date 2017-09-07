// modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BootstrapModalModule } from 'angular2-modal/plugins/bootstrap';
import { ModalModule } from 'angular2-modal';
import { HttpModule} from '@angular/http';
import { FormsModule }   from '@angular/forms';

import { AppRoutingModule } from './app-router/app-routing.module';
import { HrApplicantsModule } from './hr-applicants/hr-applicants.module';
import { HrDashboardModule } from './hr-dashboard/hr-dashboard.module';
import { LoginModule } from './app-login/app-login.module';
import { CmDashboardModule } from './cm-dashboard/cm-dashboard.module';

import { SimpleNotificationsModule } from 'angular2-notifications';

// components
import { AppComponent } from './app.component';
import { AppHeaderComponent } from './app-header/app-header.component';
import { CmRequestComponent } from './cm-request/cm-request.component';

// providers
import { requestOptionsProvider } from './common/default-request-options.service';
import { Settings } from './common/global-settings.service';
import { Lookup } from './common/lookup.service';
import { CmRequestService } from './cm-request/cm-request.service';
import { LoggedUserService } from './common/logged-user.service';
import { NotificationsService  } from 'angular2-notifications';

@NgModule({
    imports: [
        BrowserModule,
        ModalModule.forRoot(),
        BootstrapModalModule,
        HttpModule,
        FormsModule,
        // Put your custom module before the route, to prevent routing issue
        CmDashboardModule,
        HrDashboardModule,
        HrApplicantsModule,
        LoginModule,
        AppRoutingModule,
        SimpleNotificationsModule.forRoot()
    ],
    declarations: [
        AppComponent,
        AppHeaderComponent,
        CmRequestComponent
    ],
    providers: [requestOptionsProvider, Settings, Lookup, CmRequestService, LoggedUserService, NotificationsService],
    bootstrap: [AppComponent]
})

export class AppModule { }