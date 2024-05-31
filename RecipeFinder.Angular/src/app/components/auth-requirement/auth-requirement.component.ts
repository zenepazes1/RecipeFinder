import { Component, Inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialogTitle,
  MatDialogContent,
  MatDialogActions,
  MatDialogClose,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-requirement',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
  ],
  templateUrl: './auth-requirement.component.html',
  styleUrl: './auth-requirement.component.scss',
})
export class AuthRequirementComponent {
  constructor(
    public dialogRef: MatDialogRef<AuthRequirementComponent>,
    private router: Router
  ) {}

  onLoginClick(): void {
    this.dialogRef.close();
    this.router.navigate(['/register']);
  }
}
