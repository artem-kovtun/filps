import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {ConfirmModal} from '../../models/confirmModal.model';
import {ConfirmModalType} from '../../models/enums/confirmModalType.enum';

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.scss']
})
export class ConfirmModalComponent implements OnInit {

  ConfirmModelType = ConfirmModalType;

  constructor(public dialog: MatDialogRef<ConfirmModalComponent>,
              @Inject(MAT_DIALOG_DATA) public model: ConfirmModal) { }

  ngOnInit(): void {
  }

  onCancel = () => this.dialog.close(false);
  onConfirm = () => this.dialog.close(true);

}
