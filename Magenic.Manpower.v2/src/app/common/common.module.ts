import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Components
import { AppModalComponent } from '../common/app-modal';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
        AppModalComponent
    ],
    providers: [
    ],
    exports: [
        AppModalComponent
    ]
})

export class CommonAppModule { }
