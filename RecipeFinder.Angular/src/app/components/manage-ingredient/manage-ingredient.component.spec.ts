import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageIngredientComponent } from './manage-ingredient.component';

describe('ManageIngredientComponent', () => {
  let component: ManageIngredientComponent;
  let fixture: ComponentFixture<ManageIngredientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageIngredientComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ManageIngredientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
