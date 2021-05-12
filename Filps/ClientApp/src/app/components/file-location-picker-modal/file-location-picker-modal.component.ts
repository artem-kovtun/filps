import {Component, EventEmitter, Inject, Input, OnInit, Output} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FileModel} from '../../models/file.model';
import {Storage} from '../../models/enums/storage.enum';
import {StorageObject} from '../../models/storageObject.model';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-file-location-picker-modal',
  templateUrl: './file-location-picker-modal.component.html',
  styleUrls: ['./file-location-picker-modal.component.scss']
})
export class FileLocationPickerModalComponent implements OnInit {

  @Input() breadcrumb: Array<StorageObject> = [{id: '', name: 'Exams', createdOn: null}];

  Storage = Storage;
  storage = Storage.Filps;
  isDataLoaded = false;
  isAuthorized = false;

  constructor(public dialogRef: MatDialogRef<FileLocationPickerModalComponent>,
              @Inject(MAT_DIALOG_DATA) public file: FileModel,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.authState().subscribe((user) => {
      if (user !== null) {
        this.isAuthorized = true;
      }
    });
    this.isAuthorized = this.authService.isAuthorized();
    this.isDataLoaded = true;
  }

  loginWithGoogle(): void {
    this.authService.loginWithGoogle();
  }

  loginWithFacebook(): void {
    this.authService.loginWithFacebook();
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

  /*
  isBackButtonVisible = () => this.breadcrumb.length !== 0;

  back = () => {};
   */
}
