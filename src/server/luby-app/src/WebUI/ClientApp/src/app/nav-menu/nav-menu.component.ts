import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserProfileClient, UserProfileResponse } from '../web-api-client';
import { Role } from '../_models/user';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false
  isAuthenticated = false;
  userDetails: UserProfileResponse;

  constructor(private router: Router, private userProfileClient: UserProfileClient) {
    this.userProfileClient.getUserProfile().subscribe(result => {
      this.userDetails = result;
    }, error => {
      console.log(error);
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  get isAdmin() {
    return this.userDetails && this.userDetails.role === Role.Admin;
  }

  get isDev() {
    return this.userDetails && this.userDetails.role === Role.Dev;
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
