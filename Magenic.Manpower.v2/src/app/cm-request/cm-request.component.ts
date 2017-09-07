import { Component, OnInit } from '@angular/core';
import { CmRequestService, CmRequestObject, CmRequestSave } from './cm-request.service';
import { Lookup, LookupItem } from '../common/lookup.service';
import { FormsModule } from '@angular/forms';
import { NotificationsService  } from 'angular2-notifications';

@Component({
    selector: 'cm-request-component',
    templateUrl: './app/cm-request/cm-request.component.html',
    styleUrls: ['./app/cm-request/cm-request.css']
})

export class CmRequestComponent implements OnInit {

    private model: CmRequestObject;
    private options = {
        position: ["right", "top"],
        timeOut: 2000,
        lastOnBottom: false
    };
    userId: number;

    constructor(private cmRequestService: CmRequestService, private lookUpService: Lookup, private notification: NotificationsService) {
    }

    ngOnInit(): void {
        this.model = new CmRequestObject();
        this.model.numberOfHires = [];
        this.model.primaryTechnology = [];
        this.model.reasonOfRequest = [];
        this.lookUpService.getProjects().then(res => this.model.projects = res);
        this.lookUpService.getPrimarySkills().then(res => this.model.primarySkills = res);
        this.lookUpService.getRegions().then(res => this.model.regions = res);
        this.lookUpService.getPrimaryTechnologies().then(res => { this.model.primaryTechnologies = res; });
        this.model.reasonOfRequests = this.lookUpService.getReasons();
        this.lookUpService.getLevels().then(res => { this.model.levels = res; this.setNumberOfHires(); });

        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.userId = currentUser.id;
    }

    onSubmit(): void {
        var data = this.getInfo();
        this.cmRequestService.submitForm(data).subscribe(res => {
            if (res) {
                this.notification.success("", "Request Submitted!");
                setTimeout(function() {
                    location.reload();
                }, 2000);
                
            }
            else {
                this.notification.error("", "API call failed.");
            }
        },
            err => {
                console.log(err);
                //this.notification.error('', 'API call failed.');
                alert(err);
            });
    }

    private getInfo(): any {
        let data = new CmRequestSave();
        data.ProjectId = this.model.project.id;
        data.ProjectName = this.model.project.name;
        data.RegionId = this.model.requestingRegionId.id;
        data.RegionName = this.model.requestingRegionId.name;
        data.NumberOfHires = this.getNumberOfHires();
        data.ProjectedStartDate = this.formatDateTime(this.model.projectedStartedDate);
        data.JobDescription = this.model.description;
        data.PrimarySkillId = this.model.primarySkill.id;
        data.SkillName = this.model.primarySkill.name;
        data.Technologies = this.model.primaryTechnology;
        data.IsForReplacement = this.validateReason('Replacement');
        data.IsForAdditionalResource = this.validateReason('Additional resource');
        data.IsChangeRequest = this.validateReason('Change request');
        data.RequestedBy = this.userId;
        return data;
    }

    private getNumberOfHires(): any[] {
        for (let item of this.model.numberOfHires) {
            if (item == "" || item == undefined || item == null)
            {
                item = 0;
            }
        }
        return this.model.numberOfHires;
    }

    private setNumberOfHires() {
        for (let item of this.model.levels) {
            this.model.numberOfHires.push(0);
        }
    }

    private formatDateTime(date: string): any {
        var d = new Date(date);
        var curr_date = d.getDate();
        var curr_month = d.getMonth() + 1; //Months are zero based
        var curr_year = d.getFullYear();
        return curr_year + "-" + curr_month + "-" + curr_date;
    }

    private validateReason(reason: string): boolean {
        for (let item of this.model.reasonOfRequest) {
            if (item.name == reason)
                return true;
        }
        return false;
    }
    
    private addPrimaryTechnology(item: LookupItem, event: any): void {
        if (event.target.checked) {
            this.model.primaryTechnology.push(item.id);
        }
        else
        {
            var index = this.model.primaryTechnology.indexOf(item.id);
            this.model.primaryTechnology.splice(index, 1);
        }
    }

    private addReasonOfRequest(item: LookupItem, event: any): void {
        if (event.target.checked) {
            this.model.reasonOfRequest.push(item);
        }
        else {
            var index = this.model.reasonOfRequest.indexOf(item);
            this.model.reasonOfRequest.splice(index, 1);
        }
    }
}

