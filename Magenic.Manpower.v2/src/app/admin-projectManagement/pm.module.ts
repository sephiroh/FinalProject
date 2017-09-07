// Modules
import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimpleNotificationsModule } from 'angular2-notifications';

// Module Components
import { ProjectManagementComponent } from "./pm.component";
import { ProjectManagementListComponent } from "./pm-list.component";
import { ProjectManagementFormComponent } from "./pm-form.component";
import { ModalComponent } from "./pm-modal";

// External Component
import { UiSwitchComponent } from 'angular2-ui-switch/dist/ui-switch.component';

// Providers
import { ProjectManagementService } from "./pm.service";

// Module Route
import { pmRouting } from './pm-routing';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        pmRouting,
        SimpleNotificationsModule.forRoot()
    ],
    declarations: [
        ModalComponent,
        ProjectManagementListComponent,
        ProjectManagementFormComponent,
        ProjectManagementComponent,
        UiSwitchComponent
    ],
    providers: [
        ProjectManagementService
    ]
})

export class ProjectManagementModule {}
