// modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BootstrapModalModule } from 'angular2-modal/plugins/bootstrap';
import { ModalModule } from 'angular2-modal';
import { HttpModule} from '@angular/http';
import { FormsModule }   from '@angular/forms';

import { AppRoutingModule } from './app-router/app-routing.module';
import { HrApplicantsModule } from './hr-applicants/hr-applicants.module';
import { DateOrNull } from './common/date-or-null.pipe';

// components
import { AppComponent } from './app.component';
import { AppHeaderComponent } from './app-header/app-header.component';
import { HrDashboardComponent } from './hr-dashboard/hr-dashboard.component';
import { HrDashboardFilterComponent } from './hr-dashboard/hr-dashboard-filter/hr-dashboard-filter.component';
import { SampleComponent } from './sample/sample.component';

// providers
import { requestOptionsProvider } from './common/default-request-options.service';
import { Settings } from './common/global-settings.service';
import { Lookup } from './common/lookup.service';
import { DatePipe } from '@angular/common';

@NgModule({
    imports: [
        BrowserModule,
        ModalModule.forRoot(),
        BootstrapModalModule,
        HttpModule,
        FormsModule,
        // Put your custom module before the route, to prevent routing issue
        HrApplicantsModule,
        AppRoutingModule,
    ],
    declarations: [
        AppComponent,
        DateOrNull,
        AppHeaderComponent,
        HrDashboardComponent,
        HrDashboardFilterComponent,
        SampleComponent
    ],
    providers: [requestOptionsProvider, Settings, Lookup, DatePipe],
    bootstrap: [AppComponent]
})

export class AppModule { }
