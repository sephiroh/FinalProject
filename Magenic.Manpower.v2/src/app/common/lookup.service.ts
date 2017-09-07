import { Injectable } from '@angular/core';
import { Settings } from './global-settings.service';
import { Http } from '@angular/http';

@Injectable()
export class Lookup {
    name = 'Lookup Service';
    errorMessage: string;

    constructor(private http: Http, private settings: Settings) {
 
    }

    getLevels(): Promise<LookupItem[]> {
        const url = `${this.settings.apiURL}` + '/lookup/levels';
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().responseData as LookupItem[])
            .catch(this.handleError);
    }

    getPrimarySkills(): Promise<LookupItem[]> {
        const url = `${this.settings.apiURL}` + '/primarySkill';
        return this.http.get(url)
            .toPromise()
            .then(response => {
                let result: LookupItem[] = [];
                var responseData = response.json().responseData;
                for (let data of responseData) {
                    var dataToAdd = new LookupItem();
                    dataToAdd.id = data.id;
                    dataToAdd.name = data.name;
                    result.push(dataToAdd);
                }
                return result;
            })
            .catch(this.handleError);
    }

    getRegions(): Promise<LookupItem[]> {
        const url = `${this.settings.apiURL}` + '/lookup/regions';
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().responseData as LookupItem[])
            .catch(this.handleError);
    }

    getPrimaryTechnologies(): Promise<LookupItem[]> {
        const url = `${this.settings.apiURL}` + '/technologydetail/getlist';
        return this.http.get(url)
            .toPromise()
            .then(response => {
                let result: LookupItem[] = [];
                var responseData = response.json().responseData;
                for (let data of responseData) {
                    var dataToAdd = new LookupItem();
                    dataToAdd.id = data.id;
                    dataToAdd.name = data.name;
                    result.push(dataToAdd);
                }
                return result;
            });
    }

    getReasons(): LookupItem[] {
        var selectionItems = ['Replacement', 'Additional resource', 'Change request'];
        let result: LookupItem[] = [];
        for (var x = 1; x <= selectionItems.length; x++) {
            var data = new LookupItem();
            data.id = x;
            data.name = selectionItems[x - 1];
            result.push(data);
        }
        return result;
    }

    getProjects(): Promise<LookupItem[]> {
        const url = `${this.settings.apiURL}` + '/projectManagement';
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().responseData as LookupItem[])
            .catch(this.handleError);
    }

    getStatus(): Promise<LookupItem[]> {
        const url = `${this.settings.apiURL}` + '/lookup/status';
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().responseData as LookupItem[])
            .catch(this.handleError);
    }

    getApplicantStatus(): Promise<LookupItem[]> {
        const url = `${this.settings.apiURL}` + '/lookup/applicant-status';
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

export class LookupItem {
    id: number;
    name: string;
}
