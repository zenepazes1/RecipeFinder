import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageReceiptComponent } from './manage-receipt.component';

describe('ManageReceiptComponent', () => {
  let component: ManageReceiptComponent;
  let fixture: ComponentFixture<ManageReceiptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageReceiptComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ManageReceiptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
