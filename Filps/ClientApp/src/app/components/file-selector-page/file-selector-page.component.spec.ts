import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileSelectorPageComponent } from './file-selector-page.component';

describe('FileSelectorPageComponent', () => {
  let component: FileSelectorPageComponent;
  let fixture: ComponentFixture<FileSelectorPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileSelectorPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FileSelectorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
