import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { SmartLockRoutingModule } from './smart-lock-routing.module';
import { SharedModule } from '../shared-module';
import {
  MatListModule,
  MatPaginatorModule,
  MatSortModule,
  MatSidenavModule,
  MatIconModule,
  MatStepperModule,
  MatAutocompleteModule,
  MatBadgeModule,
  MatBottomSheetModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatTreeModule,
  MatTooltipModule,
  MatTabsModule,
  MatSnackBarModule,
  MatSlideToggleModule,
  MatSliderModule,
  MatSelectModule,
  MatRippleModule,
  MatRadioModule,
  MatProgressSpinnerModule,
  MatProgressBarModule,
  MatNativeDateModule,
  MatInputModule,
  MatMenuModule,
  MatGridListModule,
  MatExpansionModule,
  MatDialogModule,
  MatDividerModule,
  MatToolbarModule,
  MatTableModule
} from '@angular/material';
import { LocksService } from './services/locks.service';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { OperationsComponent } from './operations/operations.component';
import { RentersComponent } from './renters/renters.component';
import { UsersService } from './services/users.service';
import { InfoEditComponent } from './info-edit/info-edit.component';
import { WebsocketService } from './services/websocket.service';
import { ContactsComponent } from './contacts/contacts.component';
import { LanguageService } from './services/language.service';
import { SmartLockComponent } from './smart-lock.component';
import { HeaderComponent } from './header/header.component';

@NgModule({
  declarations: [
    LoginComponent, 
    RegisterComponent, 
    HomeComponent, 
    DeleteDialogComponent,
    OperationsComponent,
    RentersComponent,
    InfoEditComponent,
    ContactsComponent,
    SmartLockComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    SmartLockRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatBadgeModule,
    MatBottomSheetModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    MatTreeModule,
    MatProgressSpinnerModule,
  ],
  providers: [
    LocksService,
    UsersService,
    WebsocketService,
    LanguageService
  ],
  entryComponents: [
    DeleteDialogComponent,
    OperationsComponent,
    RentersComponent,
    InfoEditComponent
  ],
  bootstrap: [
    SmartLockComponent
  ]
})
export class SmartLockModule { }
