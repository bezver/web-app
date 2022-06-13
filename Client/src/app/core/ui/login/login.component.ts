import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, UrlSerializer } from '@angular/router';
import { User } from '../../api-models';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  hide = true;
  isRegisterFlow: boolean;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router) {
      this.isRegisterFlow = !!route.snapshot.data['registration'];
    }

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
    });
  }

  getEmailErrorMessage() {
    if (this.form.hasError('required', 'email')) {
      return 'You must enter a value';
    }
    return this.form.hasError('email', 'email') ? 'Not a valid email' : 'Error';
  }

  getPasswordErrorMessage() {
    if (this.form.hasError('required', 'email')) {
      return 'You must enter a value';
    }
    return 'Error';
  }

  submit() {
    if (this.form.invalid) {
      return;
    }
    const user: User = {
      email: this.form.controls['email'].value,
      password: this.form.controls['password'].value
    }
    if (this.isRegisterFlow) {
      this.authService.register(user);
    } else {
      this.authService.login(user);
    }
  }

}
