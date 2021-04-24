import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StorageHeaderComponent } from './storage-header.component';

describe('StorageHeaderComponent', () => {
  let component: StorageHeaderComponent;
  let fixture: ComponentFixture<StorageHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StorageHeaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StorageHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
