import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoFilesComponent } from './no-files.component';

describe('NoFilesComponent', () => {
  let component: NoFilesComponent;
  let fixture: ComponentFixture<NoFilesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NoFilesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NoFilesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
