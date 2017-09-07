import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Settings } from '../common/global-settings.service';
import { Applicant } from '../hr-applicants/applicant.model';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class HrDashboardSvc {
    errorMessage: string;

    constructor(private http: Http, private settings: Settings) { }

    fetchRequests(skill: string, project: string, statusId: string): Promise<HiringRequest[]> {
        const url = `${this.settings.apiURL}` + '/dashboard/hiringrequests?skillId='
            + `${skill}` + '&projectId=' + `${project}` + '&statusId=' + `${statusId}` + '&field=id&sort=asc';
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().responseData as HiringRequest[])
            .catch(this.handleError);
    }

    hireApplicant(applicant: Applicant): Promise<boolean> {
        const url = `${this.settings.apiURL}` + '/dashboard/hireApplicant';
        return this.http.post(url, applicant)
            .toPromise()
            .then(response => response.json().responseData as boolean)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}

export class HiringRequest {
    id: number;
    applicantLevelId: number;
    applicantLevelName: string;
    applicantName: string;
    applicantStartDate: Date;
    dateClosed: Date;
    managerName: string;
    primarySkillId: number;
    primarySkillName: string;
    projectName: string;
    requestDate: Date;
    requestNumber: string;
    status: number;

    constructor(options: any){
        this.id = options.id;
        this.primarySkillId = options.primarySkillId;
    }
}
