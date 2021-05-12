import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentRecordComponent } from './document-record.component';

describe('DocumentRecordComponent', () => {
  let component: DocumentRecordComponent;
  let fixture: ComponentFixture<DocumentRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentRecordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
