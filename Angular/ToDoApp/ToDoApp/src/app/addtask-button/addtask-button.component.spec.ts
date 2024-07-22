import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddtaskButtonComponent } from './addtask-button.component';

describe('AddtaskButtonComponent', () => {
  let component: AddtaskButtonComponent;
  let fixture: ComponentFixture<AddtaskButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddtaskButtonComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddtaskButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
