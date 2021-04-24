import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StorageAuthComponent } from './storage-auth.component';

describe('StorageAuthComponent', () => {
  let component: StorageAuthComponent;
  let fixture: ComponentFixture<StorageAuthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StorageAuthComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StorageAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
