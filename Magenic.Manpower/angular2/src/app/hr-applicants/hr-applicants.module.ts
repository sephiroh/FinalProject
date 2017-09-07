import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Components
import { ApplicantListComponent } from './applicant-list.component';
import { ApplicantForm } from './applicant-form.component';

// Providers
import { HrApplicantsService } from './applicants.service';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
        ApplicantListComponent,
        ApplicantForm
    ],
    providers: [
        HrApplicantsService
    ],
    entryComponents: [ApplicantForm]
})

export class HrApplicantsModule { }
