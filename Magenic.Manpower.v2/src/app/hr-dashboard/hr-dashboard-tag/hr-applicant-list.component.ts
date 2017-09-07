import { Component, ViewChild, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { AppModalComponent } from '../../common/app-modal';
import { HrDashboardSvc } from '../hr-dashboard.service';
import { Applicant } from '../../hr-applicants/applicant.model';
import { HrApplicantsService } from '../../hr-applicants/applicants.service';

@Component({
    selector: 'hr-applicant-list',
    templateUrl: './app/hr-dashboard/hr-dashboard-tag/hr-applicant-list.component.html',
    providers: [HrDashboardSvc, HrApplicantsService]
})

export class HrApplicantListComponent implements OnInit {
    name = 'HR Applicant List';
    applicants: Applicant[];
    selectedApplicant: Applicant;
    mode = 'Observable';

    @ViewChild(AppModalComponent) private modal: AppModalComponent;
    @Output() onHire = new EventEmitter<Applicant>();
    @Input() refId: number;

    constructor(private svc: HrDashboardSvc, private applicantSvc: HrApplicantsService) { }

    ngOnInit(): void {
    }

    showList(): void {
        this.applicantSvc.getApplicantPool(this.refId)
            .subscribe((result: any) => {
                this.applicants = result.responseData;
                this.modal.show();
            }, err => {
                console.error('An error occurred', err); // for demo purposes only
            }, () => {
                console.log('Complete!');
            });
    }

    choose(applicant: Applicant): void {
        this.selectedApplicant = applicant;
    }

    hire(): void {
        this.onHire.emit(this.selectedApplicant);
        this.modal.hide();
    }

    cancel() {
        this.modal.hide();
        this.applicants = new Array<Applicant>();
    }
}
