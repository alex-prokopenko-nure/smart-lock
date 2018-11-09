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
import { HeaderComponent } from './header/header.component';
import { LocksService } from './services/locks.service';

@NgModule({
  declarations: [
    LoginComponent, 
    RegisterComponent, 
    HomeComponent, 
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
    LocksService
  ]
})
export class SmartLockModule { }
