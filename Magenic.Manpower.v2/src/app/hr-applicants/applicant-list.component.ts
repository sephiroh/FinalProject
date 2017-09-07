import { Component, ViewContainerRef, OnInit } from '@angular/core';
import { Overlay, overlayConfigFactory } from 'angular2-modal';
import { Modal, BSModalContext } from 'angular2-modal/plugins/bootstrap';
import { ApplicantFormComponent } from './applicant-form.component';
import { HrDashboardSvc, HiringRequest } from '../hr-dashboard/hr-dashboard.service';
// Class
import { HrApplicantsService } from './applicants.service';
import { Applicant } from './applicant.model';

@Component({
    selector: 'applicant-list',
    templateUrl: './app/hr-applicants/applicant-list.component.html',
    providers: [Modal, HrDashboardSvc]
})

export class ApplicantListComponent implements OnInit {
    applicants: Array<Applicant> = [];
    applicant: Applicant = {
        id : 0,
        firstName : '',
        lastName : '',
        email : '',
        contactNumber : '',
        currentPosition : '',
        currentCompany : '',
        yearsITExperience : '',
        yearsForSpecificSkills : '',
        primarySkillId : 0,
        levelId : 0,
        status : 0,
        noticePeriod : '',
        pendingApplication: '',
        hiredDate: null
    };

    constructor(
        private hrDashboardSvc: HrDashboardSvc,
        public applicantService: HrApplicantsService,
        overlay: Overlay,
        vcRef: ViewContainerRef,
        public modal: Modal
    ) {
        overlay.defaultViewContainer = vcRef;
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

    openForm(index?: number) {
        this.modal.open(ApplicantFormComponent, overlayConfigFactory({ applicant: this.applicants[index] }, BSModalContext))
            .catch((err: any) => console.log('ERROR: ' + err))
            .then((dialog: any) => { return dialog.result })
            .then((result: any) => {

                if (!result)
                    return;

                if (index >= 0) {
                    this.applicants[index] = result;
                }
                else {
                    this.applicants.push(result);
                }

            })
            .catch((err: any) => { console.log('cancel'); });
    }

    choose(applicant: Applicant): void {
        this.applicant = applicant;
    }

    hire(applicant: any): void {
        this.hrDashboardSvc.hireApplicant(applicant).then(
            request => {
                this.getApplicants();
            },
            error => alert(error));
    }
}
