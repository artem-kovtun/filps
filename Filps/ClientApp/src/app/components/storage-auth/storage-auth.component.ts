import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Storage} from '../../models/enums/storage.enum';

@Component({
  selector: 'app-storage-auth',
  templateUrl: './storage-auth.component.html',
  styleUrls: ['./storage-auth.component.scss']
})
export class StorageAuthComponent implements OnInit {

  @Output() onAuthorize = new EventEmitter();
  @Input() storage: Storage;
  isLoginToApplication = true;

  Storage = Storage;
  drive: { storage: Storage, provider: string, icon: string };
  drives = [
    {
      storage: Storage.GoogleDrive,
      provider: 'Google',
      icon: 'google'
    },
    {
      storage: Storage.OneDrive,
      provider: 'Microsoft',
      icon: 'windows'
    },
    {
      storage: Storage.Dropbox,
      provider: 'Dropbox',
      icon: 'dropbox'
    }
  ];

  constructor() { }

  ngOnInit(): void  {
    this.drive = this.drives.filter(d => d.storage === this.storage)[0];
  }

  authorizeClick = () => this.onAuthorize.emit();

}
