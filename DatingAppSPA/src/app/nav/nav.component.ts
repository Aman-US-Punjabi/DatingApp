import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  photoUrl: string;

  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in Successfully.');
    }, error => {
      // subscribe error
      this.alertify.error(error);
    }, () => {
      // subscribe complete function
      this.router.navigate(['/members']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    this.authService.logout();
  }
}
