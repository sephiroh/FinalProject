import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { ProjectModel } from './pm.model';

import { Settings } from '../common/global-settings.service';
import { Observable } from 'rxjs/Rx';

// Import RxJs required methods
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class ProjectManagementService {

    private _url = this.settings.apiURL + '/projectManagement';

    constructor(
        private http: Http,
        private settings: Settings
    ) { }

    getProjects(): Observable<ProjectModel[]> {

        return this.http.get(this._url)
            .map((res: Response) => res.json().responseData)
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'));

    } 

    saveProject(proj: ProjectModel): Observable<ProjectModel> {
        let headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.post(this._url, proj, headers)
            .map((res: Response) => res.json().responseData)
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'));

    } 

    updateProject(proj: ProjectModel): Observable<ProjectModel> {

        let bodyString = JSON.stringify(proj); // Stringify payload
        let headers = new Headers({ 'Content-Type': 'application/json' }); // ... Set content type to JSON
        let options = new RequestOptions({ headers: headers }); // Create a request option

        return this.http.put(`${this._url}/${proj['id']}`, bodyString, options) // ...using put request
            .map((res: Response) => res.json().responseData) // ...and calling .json() on the response to return data
            .catch((error: any) => Observable.throw(error.json().error || 'Server error')); //...errors if any
    }   

    toggleActive(proj: ProjectModel): Observable<ProjectModel> {

        let bodyString = JSON.stringify(proj); 
        let headers = new Headers({ 'Content-Type': 'application/json' }); 
        let options = new RequestOptions({ headers: headers });

        return this.http.put(`${this._url}/toggle/${proj['id']}`, bodyString, options)
            .map((res: Response) => res.json().responseData)
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'));
    }
}