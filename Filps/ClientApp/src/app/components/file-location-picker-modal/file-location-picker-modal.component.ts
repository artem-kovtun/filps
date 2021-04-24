import {Component, EventEmitter, Inject, Input, OnInit, Output} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {File} from '../../models/file.model';
import {Storage} from '../../models/enums/storage.enum';

@Component({
  selector: 'app-file-location-picker-modal',
  templateUrl: './file-location-picker-modal.component.html',
  styleUrls: ['./file-location-picker-modal.component.scss']
})
export class FileLocationPickerModalComponent implements OnInit {

  Storage = Storage;

  storage = Storage.Filps;

  constructor(public dialogRef: MatDialogRef<FileLocationPickerModalComponent>,
              @Inject(MAT_DIALOG_DATA) public file: File) { }

  ngOnInit(): void {
  }

  selected = () => {
    this.dialogRef.close({
      isSelected: true,
      file: this.file
    });
  }

  cancelSelect = () => {
    this.dialogRef.close({
      isSelected: false
    });
  }
}
