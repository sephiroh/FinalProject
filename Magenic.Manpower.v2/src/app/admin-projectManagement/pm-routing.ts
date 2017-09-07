import { ModuleWithProviders } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";

import { ProjectManagementComponent } from "./pm.component";
import { ProjectManagementListComponent } from "./pm-list.component";

const pmRoutes: Routes = [
    {
        path: '',
        component: ProjectManagementComponent,

        // Future Proofing - child pages
        children: [
            {
                path: '',
                component: ProjectManagementListComponent
            }
        ]
    }
];

export const pmRouting: ModuleWithProviders = RouterModule.forChild(pmRoutes);