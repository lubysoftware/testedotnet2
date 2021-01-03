import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipsModule, MAT_CHIPS_DEFAULT_OPTIONS } from '@angular/material/chips';
import { AppComponent } from './app.component';
import { LoginFormComponent } from './auth/login-form/login-form.component';
import { MatTableModule } from '@angular/material/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { LayoutComponent } from './shared/components/layout/layout.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { AvatarModule } from 'ngx-avatar';
import { HttpInterceptorProviders } from './shared/interceptors/provider-interceptor';
import { DeveloperListComponent } from './developers/developer-list/developer-list.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { MatMenuModule } from '@angular/material/menu';
import { CustomPaginator } from './shared/translations/custom-paginator-configuration';
import { ConfirmDialogComponent } from './shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogService } from './shared/services/dialog.service';
import { MatDialogModule } from '@angular/material/dialog';
import { NgxMaskModule } from 'ngx-mask';
import { DeveloperFormComponent } from './developers/developer-form/developer-form.component';
import { ProjectListComponent } from './projects/project-list/project-list.component';
import { ProjectFormComponent } from './projects/project-form/project-form.component';
import { DeveloperRankingComponent } from './developers/developer-ranking/developer-ranking.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { WorkListComponent } from './works/work-list/work-list.component';
import { AutocompleteComponent } from './shared/components/autocomplete/autocomplete.component';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    LoginFormComponent,
    LayoutComponent,
    DeveloperListComponent,
    ConfirmDialogComponent,
    DeveloperFormComponent,
    ProjectListComponent,
    ProjectFormComponent,
    DeveloperRankingComponent,
    WorkListComponent,
    AutocompleteComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FlexLayoutModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatListModule,
    MatSidenavModule,
    MatToolbarModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatTableModule,
    MatChipsModule,
    MatCardModule,
    MatIconModule,
    MatMenuModule,
    FormsModule,
    MatSelectModule,
    MatCheckboxModule,
    MatAutocompleteModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    MatTooltipModule,
    AvatarModule,
    NgxMaskModule.forRoot(),
  ],
  providers: [
    AuthGuard,
    DialogService,
    HttpInterceptorProviders,
    { provide: MAT_CHIPS_DEFAULT_OPTIONS, useValue: { separatorKeyCodes: [ENTER, COMMA] } },
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    { provide: MatPaginatorIntl, useValue: CustomPaginator() },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
