import { Component, OnInit } from '@angular/core';
import { DeveloperDetailDto } from 'src/app/developer/dtos/developer-detail.dto';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  currentDeveloper: DeveloperDetailDto | null;

  constructor(
    public readonly authService: AuthService
  ) { 
    this.currentDeveloper = this.authService.getCurrentDeveloper();
  }

  ngOnInit(): void {
  }

}
