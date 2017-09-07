import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HrDashboardComponent } from '../hr-dashboard/hr-dashboard.component';
import { SampleComponent } from '../sample/sample.component';
import { ApplicantListComponent } from '../hr-applicants/applicant-list.component';

const routes: Routes = [{
    path: '',
    redirectTo: '/hrdashboard',
    pathMatch: 'full'
}, {
    path: 'hrdashboard',
    component: HrDashboardComponent
}, {
    path: 'sample',
    component: SampleComponent
}, {
    path: 'applicants', component: ApplicantListComponent }];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }
