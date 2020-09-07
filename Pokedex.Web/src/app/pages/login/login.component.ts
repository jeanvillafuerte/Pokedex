import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SecurityService } from 'src/providers/security.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AppUserAuth, AppUser } from 'src/security/app-user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private securitySrv: SecurityService
  ) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

  }

  onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    const user = new AppUser();
    user.user = this.loginForm.controls.username.value;
    user.password = this.loginForm.controls.password.value;

    this.securitySrv.login(user)
      .subscribe(
        data => {
          this.router.navigate(['/favorites']);
        },
        error => {
          console.error(error);
          this.loading = false;
        });
  }

}
