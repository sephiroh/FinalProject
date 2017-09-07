import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { LoginComponent } from './app-login.component';

import { LoginService } from './app-login.service';
import { AuthGuard } from '../common/auth.guard';

@NgModule({
    imports: [
        CommonModule,
        FormsModule
    ],
    declarations: [
        LoginComponent
    ],
    providers: [
        AuthGuard,
        LoginService
    ]
})

export class LoginModule { }