import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: [`
    :host ::ng-deep .pi-eye,
    :host ::ng-deep .pi-eye-slash {
        transform:scale(1.6);
        margin-right: 1rem;
        color: var(--primary-color) !important;
    }
`]
})
export class RegistrationComponent {
  RegForm: FormGroup;
  roles: { Id: string, Name: string }[] = [
    { Id: '66830E3C-322F-4D27-919A-A18C67DE715C', Name: 'Admin' },
    { Id: 'A3D2A06B-DFFA-4C1F-A71B-B82ABD59D472', Name: 'Customer' }
  ];

  password!: string;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    public layoutService: LayoutService
  ) { }

  ngOnInit() {
    this.RegForm = this.fb.group({
      firstname: ['', [Validators.required]],
      lastname: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      role: ['', Validators.required] 
    });
  }

  onSubmit() {
    if (this.RegForm.valid) {
      const formData = this.RegForm.value;

      this.authService.reglogin(formData).subscribe(
        (response) => {
          this.router.navigate(['/auth/login']);
          console.log('Registration successful', response);
        },
        (error) => {
          console.error('Registration error', error);
        }
      );
    } else {
      console.error('Form is not valid');
    }
  }
}