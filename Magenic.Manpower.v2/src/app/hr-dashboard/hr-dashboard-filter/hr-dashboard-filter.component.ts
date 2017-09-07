import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Lookup, LookupItem } from '../../common/lookup.service';

@Component({
    selector: 'hr-dashboard-filter',
    templateUrl: './app/hr-dashboard/hr-dashboard-filter/hr-dashboard-filter.component.html'
})

export class HrDashboardFilterComponent implements OnInit {
    name = 'HR Dashboard Filter';
    private errorMessage: string;
    mode = 'Observable';
    primarySkills: LookupItem[];
    projects: LookupItem[];
    status: LookupItem[];
    params: any = { skill: '0', project: '0', status: '0' };
    @Output() onFilter = new EventEmitter();

    constructor(private lookup: Lookup) { }

    ngOnInit(): void {
        this.lookup.getPrimarySkills().then(
            skills => this.primarySkills = skills,
            error => this.errorMessage = <any>error);
        this.lookup.getProjects().then(
            projects => this.projects = projects,
            error => this.errorMessage = <any>error);
        this.lookup.getStatus().then(
            status => this.status = status,
            error => this.errorMessage = <any>error);
    }

    filter(): void {
        this.onFilter.emit(this.params);
    }
}
