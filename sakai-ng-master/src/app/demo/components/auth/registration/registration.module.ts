import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { RegistrationComponent } from './registration.component';
import { RegRoutingModule } from './reg-routing.module';

@NgModule({
    imports: [
        RegRoutingModule,
        CommonModule,
        ButtonModule,
        CheckboxModule,
        InputTextModule,
        FormsModule,
        PasswordModule,
        ReactiveFormsModule
    ],
    declarations: [RegistrationComponent]
})
export class RegistrationModule { }

