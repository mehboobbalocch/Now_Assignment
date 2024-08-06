import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `]
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    valCheck: string[] = ['remember'];
    password!: string;

    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private router: Router,
        public layoutService: LayoutService
        ) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

    onSubmit() {
        if (this.loginForm.valid) {
        this.authService.userlogin(this.loginForm.value.email, this.loginForm.value.password)
          .subscribe(
            response => {
                if(response){
                  this.router.navigate(['/superadmin/dashboard']);
                  console.log('Login successful!', response);
                } else {
                  this.router.navigate(['/auth/login'])
                }
            },
            error => {
              console.error('Login error:', error);
            }
          );
      }
    }
}
