import { Component, OnInit, Output } from '@angular/core';
import { FormsModule, Validators } from '@angular/forms';
import { Observable } from 'rxjs/Rx';

// Components
import { SimpleNotificationsComponent  } from 'angular2-notifications';

// Models
import { ProjectModel } from './pm.model';

// Providers
import { ProjectManagementService } from "./pm.service";
import { NotificationsService  } from 'angular2-notifications';

// Static Services
import { AppEmitterService as emitter } from "../common/app-emitter.service";


@Component({
    selector: 'pm-list',
    templateUrl: './app/admin-projectManagement/pm-list.component.html',
    providers: [ProjectManagementService, NotificationsService]
})


export class ProjectManagementListComponent implements OnInit {

    public projects: ProjectModel[];
    public withContent: boolean = false;

    private thisId = "PM_LIST";
    private isEdit : any;
    private options = {
        position: ["right", "top"],
        timeOut: 2000,
        lastOnBottom: false
    };
    
    constructor(
        private pms: ProjectManagementService,
        private toaster: NotificationsService
    ) {
        emitter.get(this.thisId).subscribe(() => { this.getProjects(); }); 
    }

    private getProjects(): void {

        this.withContent = false;
        this.pms.getProjects()
            .subscribe(
            pmList => {
                if (pmList && pmList.length > 0) {
                    this.projects = pmList;
                    this.withContent = true;
                    this.notify();
                }
            },
            err => {
                console.log(err);
                this.toaster.error('', err.message);
            }

            );
    }

    private notify(): void {
        switch (this.isEdit) {
            case true:
                this.toaster.success("", 'Successful Updated');
                break;
            case false:
                this.toaster.success("", 'Successful Added');
                break;
            case null:
                this.toaster.success("", "Successfully Reloaded");  
                break;
            default:
                if (typeof this.isEdit === 'string') {
                    this.toaster.success("", this.isEdit);    
                }
        }
    }

    ngOnInit(): void {
        this.getProjects();
    }

    saveOk(obj: any): void {
        this.isEdit = obj.isEdit;
        emitter.get(this.thisId).emit();
    }

    toggleChange(project: ProjectModel): void {
        this.pms.toggleActive(project)
            .subscribe(
            pm => {
                if (pm) {
                    emitter.get(this.thisId).emit();
                    this.isEdit = pm.isActive ? "Successfully Activated" : "Successfully Deactivated";
                }
            },
            err => {
                console.log(err);
            }
        );
    }

    refresh(): void {
        this.isEdit = null;
        emitter.get(this.thisId).emit();
    }
}