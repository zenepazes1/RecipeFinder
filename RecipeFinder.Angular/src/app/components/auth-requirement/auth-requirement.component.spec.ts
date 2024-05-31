import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthRequirementComponent } from './auth-requirement.component';

describe('AuthRequirementComponent', () => {
  let component: AuthRequirementComponent;
  let fixture: ComponentFixture<AuthRequirementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthRequirementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AuthRequirementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
