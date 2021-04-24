import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FillMissingComponent } from './fill-missing.component';

describe('FillMissingComponent', () => {
  let component: FillMissingComponent;
  let fixture: ComponentFixture<FillMissingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FillMissingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FillMissingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
