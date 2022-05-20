import { Component } from '@angular/core';

import { AuthService } from '../services/auth.service';
import { Credentials } from '../models/credentials';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent{

  credentials = new Credentials('');

  constructor(private authSerivce: AuthService) { }

  onSubmit() {
    this.authSerivce.login(this.credentials)
      .subscribe(_ => console.log(_));
  }
}
