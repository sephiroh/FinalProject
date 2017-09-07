import { Component, OnInit } from '@angular/core';
import { DialogRef, ModalComponent, CloseGuard } from 'angular2-modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BSModalContext } from 'angular2-modal/plugins/bootstrap';

import { FormValidators } from '../common/validators/form.validator';

// Class
import { HrApplicantsService } from './applicants.service';
import { Lookup, LookupItem } from '../common/lookup.service';
import { Applicant } from './applicant.model';

import { NotificationsService } from 'angular2-notifications';

export class UpdateApplicantContext extends BSModalContext {
    constructor(
        public applicant: Applicant
    ) {
        super();
    }
}

@Component({
    selector: 'add-applicant',
    templateUrl: './app/hr-applicants/applicant-form.component.html'
})

export class ApplicantFormComponent implements OnInit {

    public formTitle: string;
    public status: number;
    context: UpdateApplicantContext;
    form: FormGroup;
    errorMessage: string;

    primarySkills: LookupItem[];
    applicantStatusList: LookupItem[];
    levels: LookupItem[];

    constructor(
        private _fb: FormBuilder,
        private applicantService: HrApplicantsService,
        private _lookupService: Lookup,
        public dialog: DialogRef<UpdateApplicantContext>,
        private notification: NotificationsService
    ) {
        this.context = dialog.context;

        this.form = this._fb.group({
            id: 0,
            firstName: ['', Validators.compose([Validators.required, Validators.maxLength(100)])],
            lastName: ['', Validators.compose([Validators.required, Validators.maxLength(100)])],
            email: ['', Validators.compose([Validators.required, Validators.maxLength(100), FormValidators.emailValidator])],
            contactNumber: ['', Validators.compose([Validators.required, Validators.maxLength(20)])],
            primarySkillId: ['', Validators.required],
            status: ['1', Validators.required],
            levelId: ['1', Validators.required],
            currentPosition: ['', Validators.compose([Validators.maxLength(50)])],
            currentCompany: ['', Validators.compose([Validators.maxLength(50)])],
            yearsITExperience: ['', Validators.compose([Validators.maxLength(10)])],
            yearsForSpecificSkills: ['', Validators.compose([Validators.maxLength(50)])],
            noticePeriod: ['', Validators.compose([Validators.maxLength(50)])],
            pendingApplication: ['', Validators.compose([Validators.maxLength(50)])]
        });

    }

    ngOnInit() {

        //This is how to get Applicant Id        
        if (this.context.applicant != null) {

            this.formTitle = 'Edit Applicant';

            //Load Applicant Status
            this._lookupService.getApplicantStatus().then(res => this.applicantStatusList = res);
            this._lookupService.getLevels().then(res => this.levels = res);

            this.form.get('id').setValue(this.context.applicant.id);
            this.form.get('firstName').setValue(this.context.applicant.firstName);
            this.form.get('lastName').setValue(this.context.applicant.lastName);
            this.form.get('email').setValue(this.context.applicant.email);
            this.form.get('contactNumber').setValue(this.context.applicant.contactNumber);
            this.form.get('primarySkillId').setValue(this.context.applicant.primarySkillId);
            this.form.get('status').setValue(this.context.applicant.status);
            this.form.get('levelId').setValue(this.context.applicant.levelId);
            this.form.get('currentPosition').setValue(this.context.applicant.currentPosition);
            this.form.get('currentCompany').setValue(this.context.applicant.currentCompany);
            this.form.get('yearsITExperience').setValue(this.context.applicant.yearsITExperience);
            this.form.get('yearsForSpecificSkills').setValue(this.context.applicant.yearsForSpecificSkills);
            this.form.get('noticePeriod').setValue(this.context.applicant.noticePeriod);
            this.form.get('pendingApplication').setValue(this.context.applicant.pendingApplication);
            this.status = this.context.applicant.status;

        }
        else {
            this.formTitle = 'Add Applicant';

            //Set applicant status to Default which is New
            this.form.get('status').setValue(1);
            this.form.get('levelId').setValue(1);
        }

        this._lookupService.getPrimarySkills().then(res => this.primarySkills = res);
    }
    save(): void {

        if (confirm('Do you want to save this applicant?')) {

            if (this.form.get('id').value == 0) {
                this.applicantService.saveAplicant(this.form.value)
                    .then((result: any) => {

                        if (result.success) {
                            this.notification.success('Create Applicant', 'Applicant has been Created.');
                            this.closeModal(result.responseData);
                        }
                        else {
                            this.notification.error('Create Applicant', result.errors[0]);
                        }
                    });
            }
            else {
                this.applicantService.updateAplicant(this.form.value)
                    .then((result: any) => {

                        if (result.success) {
                            this.notification.success('Update Applicant', 'Applicant Record has been Updated.');
                            this.closeModal(result.responseData);
                        }
                        else {
                            this.notification.error('Update Applicant', result.errors[0]);
                        }
                    });
            }

        }

    }

    closeModal(applicant?: Applicant): void {
        this.dialog.close(applicant);
    }
}