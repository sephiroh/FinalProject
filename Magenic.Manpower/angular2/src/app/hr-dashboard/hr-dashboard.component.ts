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
    collection: HrDashboardSvc;
    errorMessage: string;
    mode = 'Observable';
    skill: string = '0';
    project: string = '0';
    primarySkills: LookupItem[];
    projects: LookupItem[];

    constructor(private hiringRequests: HrDashboardSvc) { }

    private populate(): void {
        this.hiringRequests.fetch(this.skill, this.project).then(
            requests => this.requests = requests,
            error => this.errorMessage = <any>error);
    }

    filter(params: any): void {
        this.skill = params.skill;
        this.project = params.project;
        this.populate();
    }

    ngOnInit(): void {
        this.populate();
    }
}
