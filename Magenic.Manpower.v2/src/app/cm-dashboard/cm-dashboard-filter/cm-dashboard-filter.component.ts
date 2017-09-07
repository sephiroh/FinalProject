import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Lookup, LookupItem } from '../../common/lookup.service';

@Component({
    selector: 'cm-dashboard-filter',
    templateUrl: './app/cm-dashboard/cm-dashboard-filter/cm-dashboard-filter.component.html'
})

export class CmDashboardFilterComponent implements OnInit {
    name = 'CM Dashboard Filter';
    private errorMessage: string;
    mode = 'Observable';
    primarySkills: LookupItem[];
    projects: LookupItem[];
    statuses: LookupItem[];
    params: any = { skill: '0', project: '0', status: '0'};
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
            statuses => this.statuses = statuses,
            error => this.errorMessage = <any>error);
    }

    filter(): void {
        this.onFilter.emit(this.params);
    }
}
