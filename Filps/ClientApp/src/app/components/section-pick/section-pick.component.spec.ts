import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SectionPickComponent } from './section-pick.component';

describe('SectionPickComponent', () => {
  let component: SectionPickComponent;
  let fixture: ComponentFixture<SectionPickComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SectionPickComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SectionPickComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
