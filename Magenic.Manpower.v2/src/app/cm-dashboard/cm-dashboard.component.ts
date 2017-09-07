import { Component, OnInit } from '@angular/core';
import { CmDashboardService, HiringRequest, RefNumberReason } from './cm-dashboard.service';
import { LookupItem } from '../common/lookup.service';

// Components
import { SimpleNotificationsComponent  } from 'angular2-notifications';
import { NotificationsService  } from 'angular2-notifications';

@Component({
    selector: 'cm-dashboard',
    templateUrl: './app/cm-dashboard/cm-dashboard.component.html',
    styleUrls: ['./app/cm-dashboard/cm-dashboard.css'],
    providers: [CmDashboardService, NotificationsService]
})

export class CmDashboardComponent implements OnInit {
    name = 'CM Dashboard';
    requests: HiringRequest[];
    collection: CmDashboardService;
    errorMessage: string;
    mode = 'Observable';
    managerId: number;
    skill: string = '0';
    project: string = '0';
    status: string = '0';
    primarySkills: LookupItem[];
    projects: LookupItem[];
    request: HiringRequest;

    private options = {
        position: ["right", "top"],
        timeOut: 2000,
        lastOnBottom: false
    };

    constructor(private projectRequests: CmDashboardService, private toaster: NotificationsService) {
    }

    private populate(): void {
        this.projectRequests.fetch(this.managerId, this.skill, this.project, this.status).then(
            requests => this.requests = requests,
            error => this.toaster.error('', error.message));
    }

    filter(params: any): void {
        this.skill = params.skill;
        this.project = params.project;
        this.status = params.status;
        this.populate();
    }

    toggleRequest(request: HiringRequest): void {
        this.request = request;
    }

    cancel(refNum: any): void {
        this.projectRequests.toggleRequestStatus(refNum)
            .then(reply => {
                this.toaster.success("", 'Successfully Cancelled');
                this.populate();
            },
            error => this.toaster.error('', error.message));
    }

    ngOnInit(): void {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.managerId = currentUser.id;
        this.populate();
    }
}
