import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CommonAppModule } from '../common/common.module';

// Components
import { HrDashboardComponent } from './hr-dashboard.component';
import { HrDashboardFilterComponent } from './hr-dashboard-filter/hr-dashboard-filter.component';
import { HrApplicantListComponent } from './hr-dashboard-tag/hr-applicant-list.component';
import { DateOrNullModule } from '../common/date-or-null.module';

// Providers
import { HrDashboardSvc } from './hr-dashboard.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        CommonAppModule,
        DateOrNullModule.forRoot()
    ],
    declarations: [
        HrDashboardComponent,
        HrDashboardFilterComponent,
        HrApplicantListComponent
    ],
    providers: [
        HrDashboardSvc
    ]
})

export class HrDashboardModule { }
