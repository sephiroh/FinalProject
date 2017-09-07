import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Headers } from '@angular/http';
import { Settings } from '../common/global-settings.service';
import { Applicant } from './applicant.model';

import 'rxjs/add/operator/map';

@Injectable()
export class HrApplicantsService {

    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private _http: Http, private _settings: Settings) {

    };

    getAllApplicants(): Observable<Array<Applicant>> {

        const url = `${this._settings.apiURL}` + '/applicants';

        return this._http.get(url)
            .map(res => {
                return res.json();
            });
    };

    getApplicantPool(refId: number): Observable<Array<Applicant>> {

        const url = `${this._settings.apiURL}` + '/dashboard/hiringrequests/' + `${refId}` + '/applicants';

        return this._http.get(url)
            .map(res => {
                return res.json();
            });
    };

    getReferencePool(appId: number): Observable<Array<Applicant>> {

        const url = `${this._settings.apiURL}` + '/applicants/' + `${appId}` + '/refNos';

        return this._http.get(url)
            .map(res => {
                return res.json();
            });
    };

    saveAplicant(applicant: Applicant) {
        const url = `${this._settings.apiURL}` + '/applicants/';

        return this._http
            .post(url, applicant, { headers: this.headers })
            .toPromise()
            .then(result => result.json())
            .catch(this.handleError);
    };

    updateAplicant(applicant: Applicant): Promise<Applicant> {
        const url = `${this._settings.apiURL}` + '/applicants/' + applicant.id;

        return this._http
            .put(url, JSON.stringify(applicant), { headers: this.headers })
            .toPromise()
            .then(result => result.json())
            .catch(this.handleError);
    };

    private handleError(error: any): Promise<any> {
        // console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    };

}
