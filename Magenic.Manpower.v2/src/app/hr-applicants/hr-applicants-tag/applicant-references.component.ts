import { Component, ViewChild, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { AppModalComponent } from '../../common/app-modal';
import { HrDashboardSvc } from '../../hr-dashboard/hr-dashboard.service';
import { Applicant } from '../applicant.model';
import { HrApplicantsService } from '../applicants.service';

@Component({
    selector: 'applicant-references',
    templateUrl: './app/hr-applicants/hr-applicants-tag/applicant-references.component.html',
    providers: [HrDashboardSvc, HrApplicantsService]
})

export class ApplicantReferencesComponent implements OnInit {
    name = 'Applicant References';
    refs: Applicant[];
    selectedRef: Applicant;
    mode = 'Observable';

    @ViewChild(AppModalComponent) private modal: AppModalComponent;
    @Output() onHire = new EventEmitter<Applicant>();
    @Input() appId: number;

    constructor(private svc: HrDashboardSvc, private applicantSvc: HrApplicantsService) { }

    ngOnInit(): void {
    }

    showList(): void {
        this.applicantSvc.getReferencePool(this.appId)
            .subscribe((result: any) => {
                this.refs = result.responseData;
                this.modal.show();
            }, err => {
                console.error('An error occurred', err); // for demo purposes only
            }, () => {
                console.log('Complete!');
            });
    }

    choose(ref: Applicant): void {
        this.selectedRef = ref;
    }

    hire(): void {
        this.onHire.emit(this.selectedRef);
        this.modal.hide();
    }

    cancel() {
        this.modal.hide();
        this.refs = new Array<Applicant>();
    }
}
