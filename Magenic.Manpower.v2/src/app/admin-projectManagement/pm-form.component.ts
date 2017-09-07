import { Component, OnInit, AfterViewChecked, Input, Output, ViewChild, EventEmitter  } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, FormsModule, NgForm } from '@angular/forms';

// Model
import { ProjectModel } from './pm.model';

// Component
import { ModalComponent } from "./pm-modal";
import { Observable } from 'rxjs/Rx';

// Providers
import { ProjectManagementService } from "./pm.service";
import { AppEmitterService } from "../common/app-emitter.service";
import { NotificationsService  } from 'angular2-notifications';

/// Common Functions
import { deepCopy } from "./pm-common.function";


@Component({
    selector: 'pm-form',
    templateUrl: './app/admin-projectManagement/pm-form.component.html',
    providers: [ProjectManagementService]
})


export class ProjectManagementFormComponent implements OnInit, AfterViewChecked {

    

    @ViewChild(ModalComponent) private modal: ModalComponent;
    @ViewChild('projectForm') private projForm: NgForm;
    @Output() public saveOk = new EventEmitter<any>();

    private project: ProjectModel;
    private projectCopy: ProjectModel;
    private isEdit: boolean;
    private validationMessages = {
        'name': {
            'required': '*Name is required.',
            'invalid': '*The name provided is not valid.'
        },
        'description': {
            'required': '*Description is required.'
        }
    };

    public saveCaption: string;
    public saveDisabled = false;
    public pmcForm: NgForm;

    public formErrors = {
        'name': '',
        'description': ''
    };

    constructor(
        private pms: ProjectManagementService,
        private ns: NotificationsService
    ) { }

    ngOnInit(): void {
        this.project = new ProjectModel();
    }

    ngAfterViewChecked() {
        this.formChanged();
    }

    private formChanged() {
        if (this.projForm === this.pmcForm) { return; }
        this.pmcForm = this.projForm;
        if (this.pmcForm) {
            this.pmcForm.valueChanges
                .subscribe(data => this.onValidate(data));
        }
    }

    private save(): void {
        this.pms.saveProject(this.project)
            .subscribe(
            pm => {
                if (pm !== undefined) {
                    this.saveOk.emit({ isEdit: this.isEdit, project: pm });
                    this.modal.hide();
                } else {
                    this.ns.error('', 'Invalid Project');
                }
            },
            err => {
                console.log(err);
                this.ns.error('', 'Adding Failed');
            }
            );
    }

    private update(): void {

        this.pms.updateProject(this.project)
            .subscribe(
            pm => {
                if (pm !== undefined) {
                    this.saveOk.emit({ isEdit: this.isEdit, project: pm });
                    this.modal.hide();
                } else {
                    this.project = deepCopy(this.projectCopy);
                    this.ns.error('', 'Invalid Project');
                }
            },
            err => {
                console.log(err);
                this.ns.error('', 'Updating Failed');
            });
    }

    private onValidate(data?: any) {
        if (!this.pmcForm) {
            return;
        }

        let check: boolean[] = [];
        const form = this.pmcForm.form;

        for (const field in this.formErrors) {
            this.formErrors[field] = '';
            const control = form.get(field);

            if (control && (control.dirty && !control.valid)) {
                const messages = this.validationMessages[field];
                for (const key in control.errors) {
                    this.formErrors[field] += messages[key] + ' ';
                }
            }

            if (this.isEdit) {
                if (control && control.dirty && (control.value !== this.projectCopy[field])) {
                    check.push(true);
                } 
            } 
        }

        if (!this.isEdit) 
        if (!form.valid) {
            this.saveDisabled = true;
        } else {
            this.saveDisabled = false;
        }

        if (this.isEdit) {
            if (!form.valid) {
                this.saveDisabled = true;
                return;
            }

            if (check && check.filter(x => x === true)[0]) {
                this.saveDisabled = false;
            } else {
                this.saveDisabled = true;
            }
        }
    }

    disableSave(): boolean {
        return this.saveDisabled;
    }

    add(): void {
        this.projectCopy = new ProjectModel();
        this.isEdit = false;
        this.project = new ProjectModel();
        this.saveCaption = "Save";
        this.modal.show();
    }

    edit(proj: ProjectModel): void {
        this.projectCopy = proj;
        this.isEdit = true;
        this.project = deepCopy(proj); 
        this.saveCaption = "Update";
        this.modal.show();
    }

    cancel(ff: FormGroup): void {
        this.modal.hide();
        ff.reset();
    }

    submit(): void {
        if (this.isEdit) {
            this.update();
            return;
        }
        this.save();
    }
}