import { Injectable } from '@angular/core';
import { Settings } from './global-settings.service';
import { Http } from '@angular/http';

@Injectable()
export class Lookup {
    name = 'Lookup Service';
    levels: LookupItem[];
    errorMessage: string;

    constructor(private http: Http, private settings: Settings) {
        this.initialize();
    }

    private loadLevels(): Promise<LookupItem[]> {
        const url = `${this.settings.apiURL}` + '/lookup/levels';
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().responseData as LookupItem[])
            .catch(this.handleError);
    }

    initialize(): void {
        this.loadLevels().then(
            levels => this.levels = levels,
            error => this.errorMessage = <any>error);
    }

    getPrimarySkills(): Promise<LookupItem[]>{
        const url = `${this.settings.apiURL}` + '/primarySkill';
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().responseData as LookupItem[])
            .catch(this.handleError);
    }

    getProjects(): Promise<LookupItem[]>{
        const url = `${this.settings.apiURL}` + '/projectManagement';
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().responseData as LookupItem[])
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}

export class LookupItem{
    id: number;
    name: string;
}
