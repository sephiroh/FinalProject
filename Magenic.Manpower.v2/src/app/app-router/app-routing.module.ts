import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { HrDashboardComponent } from '../hr-dashboard/hr-dashboard.component';
import { CmDashboardComponent } from '../cm-dashboard/cm-dashboard.component';
import { CmRequestComponent } from '../cm-request/cm-request.component';
import { ApplicantListComponent } from '../hr-applicants/applicant-list.component';
import { LoginComponent } from '../app-login/app-login.component';

import { AuthGuard } from '../common/auth.guard';

const _routes: Routes = [{
    path: '',
    redirectTo: '/login',
    pathMatch: 'full'
},
    {
        path: 'hrdashboard',
        component: HrDashboardComponent,
        data: { isRoot: true, title: 'HR Dashboard', permissions: ['Tag Applicants'] },
        canActivate: [AuthGuard]
    },
    {
        path: 'cmdashboard',
        component: CmDashboardComponent,
        data: { isRoot: true, title: 'CM Dashboard', permissions: ['Request Manpower', 'Cancel Request'] },
        canActivate: [AuthGuard]
    },
    {
        path: 'cmrequest',
        component: CmRequestComponent,
        data: { isRoot: true, title: 'CM Request', permissions: ['Request Manpower'] },
        canActivate: [AuthGuard]
    },
    {
        path: 'applicants', component: ApplicantListComponent,
        data: { isRoot: true, title: 'Applicants', permissions: ['Tag Applicants'] },
        canActivate: [AuthGuard]
    },
    {
        // Lazy loading is applied here
        path: 'projectManagement',
        loadChildren: './app/admin-projectManagement/pm.module#ProjectManagementModule',
        data: { isRoot: true, title: 'Project Management', permissions: ['Request Manpower'] },
        canActivate: [AuthGuard]
    },
    {
        path: 'login', component: LoginComponent,
        data: { isRoot: false, title: 'Login' }
    }
];

@NgModule({
    imports: [RouterModule.forRoot(_routes)],
    exports: [RouterModule]
})

export class AppRoutingModule {
    get Routes() {
        return _routes;
    }
}