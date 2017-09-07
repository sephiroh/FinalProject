import 'rxjs/Rx';
import {Observable} from 'rxjs/Observable';
import {Injectable, Inject, Optional, OpaqueToken} from '@angular/core';
import {Http, Headers, Response, RequestOptionsArgs} from '@angular/http';
import { Settings } from '../common/global-settings.service';
import { LookupItem } from '../common/lookup.service';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';

@Injectable()
export class CmRequestService {
    protected jsonParseReviver: (key: string, value: any) => any = undefined;

    constructor(private http: Http, private settings: Settings) {
    }

    submitForm(form: any): Observable<boolean> {
        let url_ = this.settings.apiURL + "/request/submit";
        var data = JSON.stringify(form);
        //return this.http.request(url_, {
        //    body: data,
        //    method: "post",
        //    headers: new Headers({
        //        "Content-Type": "application/json; charset=UTF-8",
        //        "Accept": "application/json; charset=UTF-8"
        //    })
        //}).map((response: Response) => {
        //    return response.json().success;
        //    }).catch((error: any) => Observable.throw(error.json().error || 'Server error'));

        return this.http.post(url_, data,
            new Headers({
                "Content-Type": "application/json; charset=UTF-8",
            })
        ).map((response: Response) => {
            return response.json().success;
        }).catch((error: any) => Observable.throw(error.json().error || 'Server error'));
    }

    getProjects(): Observable<any[]> {
        let url_ = this.settings.apiURL + "/projectManagement";
        const content_ = "";

        return this.http.request(url_, {
            body: content_,
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
            //}).toPromise()
            //    .then(response => response.json().responseData as any[])
        }).map((response) => {
            return this.processProjectList(response);
        });

    }

    private processProjectList(response: Response): any[] {
        const responseText = response.text();
        const status = response.status;

        if (status === 200) {
            let result200: any[] = null;
            let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            if (resultData200.success && resultData200.responseData.length > 0) {
                result200 = resultData200.responseData;
            }
            return result200;
        } else if (status !== 200 && status !== 204) {
            alert(response);
        }
        return null;
    }
}

export class CmRequestObject {
    project: LookupItem;
    projects: LookupItem[];
    primarySkill: LookupItem;
    primarySkills: LookupItem[];
    regions: LookupItem[];
    requestingRegionId: LookupItem;
    projectedStartedDate: string;
    description: string;
    primaryTechnologies: LookupItem[];
    primaryTechnology: any[];
    reasonOfRequests: LookupItem[];
    reasonOfRequest: LookupItem[];
    levels: LookupItem[];
    numberOfHires: any[];
}

export class CmRequestSave {
    ProjectId: number;
    ProjectName: string;
    RegionId: number;
    RegionName: string;
    NumberOfHires: any[];
    ProjectedStartDate: any;
    JobDescription: string;
    PrimarySkillId: number;
    SkillName: string;
    Technologies: any[];
    IsForReplacement: boolean;
    IsForAdditionalResource: boolean;
    IsChangeRequest: boolean;
    RequestedBy: number;
}