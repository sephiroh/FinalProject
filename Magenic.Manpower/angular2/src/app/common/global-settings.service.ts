import { Injectable } from '@angular/core';

const apiURL = 'http://localhost:55022/api';

@Injectable()
export class Settings {
    get apiURL() {
        return apiURL;
    }
}
