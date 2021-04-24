import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileLocationPickerModalComponent } from './file-location-picker-modal.component';

describe('FileLocationPickerModalComponent', () => {
  let component: FileLocationPickerModalComponent;
  let fixture: ComponentFixture<FileLocationPickerModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileLocationPickerModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FileLocationPickerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
