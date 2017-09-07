import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CommonAppModule } from '../common/common.module';
import { SimpleNotificationsModule } from 'angular2-notifications';

// Components
import { CmDashboardComponent } from './cm-dashboard.component';
import { CmDashboardFilterComponent } from './cm-dashboard-filter/cm-dashboard-filter.component';
import { CmDashboardReasonComponent } from './cm-dashboard-tag/cm-dashboard-reason.component';
import { DateOrNullModule } from '../common/date-or-null.module';

// Providers
import { CmDashboardService } from './cm-dashboard.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        CommonAppModule,
        DateOrNullModule.forRoot(),
        SimpleNotificationsModule.forRoot()
    ],
    declarations: [
        CmDashboardComponent,
        CmDashboardFilterComponent,
        CmDashboardReasonComponent
    ],
    providers: [
        CmDashboardService
    ]
})

export class CmDashboardModule { }
