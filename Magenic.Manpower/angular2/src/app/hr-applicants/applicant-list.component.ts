import {Component, ViewContainerRef, OnInit } from '@angular/core';
import {Modal} from 'angular2-modal/plugins/bootstrap';
import {ApplicantForm} from './applicant-form.component';

// Class
import { Applicant, HrApplicantsService } from './applicants.service';

@Component({
    selector: 'applicant-list',
    templateUrl: './app/hr-applicants/applicant-list.component.html',
    providers: [Modal]
})

export class ApplicantListComponent implements OnInit {

    applicants: Array<Applicant> = [];

    constructor(
        public applicantService: HrApplicantsService,
        vcRef: ViewContainerRef,
        public modal: Modal
    ) {
        modal.overlay.defaultViewContainer = vcRef;
    }

    ngOnInit() {

       this.getApplicants();

    }


    getApplicants(): void {

        this.applicantService.getAllApplicants()
        .subscribe((result: any) => {
            this.applicants = result.responseData;
        }, err => {
            console.error('An error occurred', err); // for demo purposes only
        }, () => {
            console.log('Complete!');
        });
    }

    openForm() {
        return this.modal.open(ApplicantForm);
    }
}