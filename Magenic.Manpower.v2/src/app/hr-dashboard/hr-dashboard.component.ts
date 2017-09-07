import { Component, OnInit } from '@angular/core';
import { HrDashboardSvc, HiringRequest } from './hr-dashboard.service';
import { LookupItem } from '../common/lookup.service';

@Component({
    selector: 'hr-dashboard',
    templateUrl: './app/hr-dashboard/hr-dashboard.component.html',
    styleUrls: ['./app/hr-dashboard/hr-dashboard.css'],
    providers: [HrDashboardSvc]
})

export class HrDashboardComponent implements OnInit {
    name = 'HR Dashboard';
    requests: HiringRequest[];
    errorMessage: string;
    mode = 'Observable';
    skill: string = '0';
    project: string = '0';
    status: string = '0';
    request: HiringRequest = new HiringRequest({ options: { id: 0, primarySkillId: 0 } });
    primarySkills: LookupItem[];
    projects: LookupItem[];

    constructor(private hrDashboardSvc: HrDashboardSvc) { }

    private populate(): void {
        this.hrDashboardSvc.fetchRequests(this.skill, this.project, this.status).then(
            requests => this.requests = requests,
            error => this.errorMessage = <any>error);
    }

    filter(params: any): void {
        this.skill = params.skill;
        this.project = params.project;
        this.status = params.status;
        this.populate();
    }

    ngOnInit(): void {
        this.populate();
    }

    choose(request: HiringRequest): void {
        this.request = request;
    }

    hire(applicant: any): void {
        this.hrDashboardSvc.hireApplicant(applicant).then(
            request => {
                this.populate();
            },
            error => this.errorMessage = <any>error);
    }
}
