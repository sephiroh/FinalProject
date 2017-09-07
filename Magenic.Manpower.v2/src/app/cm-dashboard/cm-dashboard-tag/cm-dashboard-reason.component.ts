import { Component, ViewChild, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { AppModalComponent } from '../../common/app-modal';
import { CmDashboardService, HiringRequest, RefNumberReason } from '../cm-dashboard.service';

@Component({
    selector: 'cm-dashboard-reason',
    templateUrl: './app/cm-dashboard/cm-dashboard-tag/cm-dashboard-reason.component.html',
    providers: [CmDashboardService]
})

export class CmDashboardReasonComponent implements OnInit {
    name = 'CM Dashboard Reason';
    mode = 'Observable';
    refNumReason: RefNumberReason = new RefNumberReason();

    @ViewChild(AppModalComponent) private modal: AppModalComponent;
    @Output() onSubmit = new EventEmitter<RefNumberReason>();
    @Input() request: HiringRequest;

    constructor(private service: CmDashboardService) { }

    ngOnInit(): void {
    }

    showModal() {
        this.refNumReason = new RefNumberReason();
        this.refNumReason.refNumberId = this.request.id;
        this.refNumReason.newStatus = 3; // set to CANCEL
        this.modal.show();
    }

    submitCancellation(): void {
        this.onSubmit.emit(this.refNumReason);
        this.modal.hide();
    }

    cancel() {
        this.refNumReason.reason = '';
        this.modal.hide();
    }
}
