import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Settings } from '../common/global-settings.service';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class CmDashboardService {
    errorMessage: string;

    constructor(private http: Http, private settings: Settings) { }

    fetch(managerId: number, skill: string, project: string, statusId: string): Promise<HiringRequest[]> {
        const url = `${this.settings.apiURL}` + '/dashboard/projectrequests?skillId='
            + `${skill}` + '&projectId=' + `${project}` + '&statusId='
            + `${statusId}` + '&requestor=' + `${managerId}` + '&field=id&sort=asc';

        return this.http.get(url)
            .toPromise()
            .then(response => response.json().responseData as HiringRequest[])
            .catch(this.handleError);
    }

    toggleRequestStatus(refNum: RefNumberReason): Promise<any> {
        let requestStatus = {
            RefNumberId: refNum.refNumberId,
            Reason: refNum.reason,
            NewStatus: refNum.newStatus
        }

        let headers = new Headers({ 'Content-Type': 'application/json' });

        const url = this.settings.apiURL + '/dashboard/togglerefnumberstatus';
        return this.http.post(url, JSON.stringify(requestStatus), headers)
            .toPromise()
            .then(response => response.json().responseData as RefNumberReason)
            .catch(this.handleError);
    }


    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}

export class HiringRequest {
    id: number;
    applicantLevelName: string;
    applicantName: string;
    applicantStartDate: Date;
    dateClosed: Date;
    managerName: string;
    managerId: number;
    primarySkillName: string;
    projectName: string;
    requestDate: Date;
    requestNumber: string;
    status: number;
}

export class RefNumberReason {
    refNumberId: number;
    reason: string;
    newStatus: number;
}
