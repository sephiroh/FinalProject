import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonAppModule } from '../common/common.module';

// Components
import { ApplicantListComponent } from './applicant-list.component';
import { ApplicantFormComponent } from './applicant-form.component';
import { ApplicantReferencesComponent } from './hr-applicants-tag/applicant-references.component';

// Providers
import { HrApplicantsService } from './applicants.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        CommonAppModule
    ],
    declarations: [
        ApplicantListComponent,
        ApplicantFormComponent,
        ApplicantReferencesComponent
    ],
    providers: [
        HrApplicantsService
    ],
    entryComponents: [ApplicantFormComponent]
})

export class HrApplicantsModule { }
