import {Component} from '@angular/core';
import {DialogRef, ModalComponent, CloseGuard} from 'angular2-modal';
import {BSModalContext} from 'angular2-modal/plugins/bootstrap';

@Component({
    selector: 'add-applicant',
    templateUrl: './app/hr-applicants/applicant-form.component.html'
})

export class ApplicantForm extends BSModalContext {
}