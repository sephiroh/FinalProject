import { Injectable } from '@angular/core';

const apiURL = 'http://localhost:55022/api';

//const permissionList = {
//    APP_MGMT: 'Application Management',
//    REQUEST_MANPOWER: 'Request Manpower',
//    CANCEL_REQUEST: 'Cancel Request',
//    TAG_APPLICANTS: 'Tag Applicants'
//};

@Injectable()
export class Settings {
    get apiURL() {
        return apiURL;
    }

    //get permissionList() {
    //    return permissionList;
    //}
}
