import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http } from '@angular/http';
import { Settings } from '../common/global-settings.service';

import 'rxjs/add/operator/map';

export class Applicant {
    constructor(
        public Id: number,
        public firstName: string,
        public lastName: string,
        public email: string,
        public contactNumber: string
    ) {}
}

@Injectable()
export class HrApplicantsService {

    constructor(private _http: Http, private _settings: Settings){

    }

    getAllApplicants(): Observable<Array<Applicant>> {

        const url = `${this._settings.apiURL}` + '/applicants';

        return this._http.get(url)
            .map(res => {
                return res.json();
            });
    }

}
