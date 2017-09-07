import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Settings } from '../common/global-settings.service';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class HrDashboardSvc {
    errorMessage: string;

    constructor(private http: Http, private settings: Settings) { }

    fetch(skill: string, project: string): Promise<HiringRequest[]> {
        const url = `${this.settings.apiURL}` + '/dashboard/hiringrequests?skillId='
            + `${skill}` + '&projectId=' + `${project}` + '&field=id&sort=asc';
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().responseData as HiringRequest[])
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}

export class HiringRequest {
    applicantLevelName: string;
    applicantName: string;
    applicantStartDate: Date;
    dateClosed: Date;
    managerName: string;
    primarySkillName: string;
    projectName: string;
    requestDate: Date;
    requestNumber: string;
    status: number;
}
