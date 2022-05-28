import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';
import { Credentials } from '../../models/credentials';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent{

  credentials = new Credentials('');

  constructor(
    private router: Router,
    private authSerivce: AuthService
    ) { }

  onSubmit() {
    this.authSerivce.login(this.credentials)
      .subscribe(success => {
        if (!!success)
          this.router.navigate(['/articles'])
      });
  }
}
