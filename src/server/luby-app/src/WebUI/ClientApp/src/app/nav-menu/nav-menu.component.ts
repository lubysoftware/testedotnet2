import { Component } from '@angular/core'; 
import { AuthService } from '../auth/auth.service';
import { UserProfileClient, UserProfileResponse } from '../web-api-client';
import { Role } from '../_models/role';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false
  isAuthenticated = false;
  userDetails: UserProfileResponse;
  isAdmin: boolean;
  isDev: boolean; 

  constructor(private userProfileClient: UserProfileClient, private authService: AuthService) { 
    this.authService.loggedIn.subscribe(value => {
      this.userProfileClient.getUserProfile().subscribe(result => {
        this.setUserProfile(result);
      }, error => {
        this.setUserProfile(null);
        console.log(error);
      });
    }); 
  }

  setUserProfile(result: UserProfileResponse) {
    this.userDetails = result;
    this.isAdmin = this.userDetails && this.userDetails.role === Role.Admin;
    this.isDev = this.userDetails && this.userDetails.role === Role.Dev;
  }

  collapse() {
    this.isExpanded = false;
  }

  onLogout() {
    this.authService.logout();
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
