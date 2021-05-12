import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {StorageAuthentication} from '../../models/storageAuthentication.model';
import {ActivatedRoute} from '@angular/router';
import {TranslateService} from '@ngx-translate/core';
import {Storage} from '../../models/enums/storage.enum';
import {FileModel} from '../../models/file.model';
import {UserService} from '../../services/user.service';
import {SocialAuthService, SocialUser} from 'angularx-social-login';
import {FileService} from '../../services/file.service';
import {HttpResponse} from '@angular/common/http';
import {FileMetadata} from '../../models/fileMetadata.model';
import {GetFilesQuery} from '../../models/getFilesQuery.model';
import {PagedList} from '../../models/pagedList.model';
import {ConfirmModalComponent} from '../confirm-modal/confirm-modal.component';
import {ConfirmModalType} from '../../models/enums/confirmModalType.enum';
import {MatDialog} from '@angular/material/dialog';
import {saveAs} from 'file-saver';
import {FileSystemFileEntry, NgxFileDropEntry} from 'ngx-file-drop';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-file-selector-page',
  templateUrl: './file-selector-page.component.html',
  styleUrls: ['./file-selector-page.component.scss']
})
export class FileSelectorPageComponent implements OnInit {

  @Output() onFileChange = new EventEmitter<FileModel>();

  Storage = Storage;
  isDataLoaded = false;
  isSelected = false;
  isLoginToApplication = true;
  isAuthorized = false;
  user: SocialUser;
  tab: number;
  language: string;

  languages = [
    {
      code: 'en',
      label: 'English'
    },
    {
      code: 'ru',
      label: 'Русский'
    }
  ];
  authData = new Array<StorageAuthentication>();


  filesPagedList = new PagedList<FileMetadata>();
  getFilesQuery = new GetFilesQuery(1, 10);

  constructor(private route: ActivatedRoute,
              private userService: UserService,
              private fileService: FileService,
              private translate: TranslateService,
              private authService: AuthService,
              public dialog: MatDialog){
    this.route.queryParams.subscribe(params => {
      const initialTab = +params.tab;
      this.tab = (initialTab < 1 || initialTab > 3) ? 0 : initialTab;
    });

    this.language = localStorage.getItem('lang');
    if (this.language == null){
      localStorage.setItem('lang', 'en');
      this.language = 'en';
    }

    translate.setDefaultLang(this.language);
    translate.currentLang = this.language;
  }

  ngOnInit(): void {
    this.authService.authState().subscribe((user) => {
      if (user !== null) {
        this.user = user;
        this.isAuthorized = true;
        this.getFiles();
      }

      this.isDataLoaded = true;
    });
  }

  loginWithGoogle(): void {
    this.authService.loginWithGoogle();
  }

  loginWithFacebook(): void {
    this.authService.loginWithFacebook();
  }

  logOut(): void {
    this.authService.logOut().then(_ => {
      this.isAuthorized = !this.isAuthorized;
    });
  }

  onLanguageChange = (newLanguage: string) => {
    this.language = newLanguage;
    this.translate.use(newLanguage);
    localStorage.setItem('lang', newLanguage);
  }

  currentLanguageLabel = () => {
    return this.languages.filter(l => l.code === this.language)[0].label;
  }

  tabSelected = (i: number) => {
    const storageData = this.authData.filter(ad => ad.storage === i)[0];
    if (storageData !== undefined){
      storageData.isLoaded = true;
    }
  }

  isLoaded = (storage: Storage) => {
    const storageData = this.authData.filter(ad => ad.storage === storage)[0];
    if (storageData === undefined) return false;
    return storageData.isLoaded;
  }

  isStorageAuthorized = (storage: Storage) => {
    const storageData = this.authData.filter(ad => ad.storage === storage)[0];
    if (storageData === undefined) return false;
    return storageData.isAuthorized;
  }

  createNewBlankDocument = () => this.onFileChange.emit(new FileModel());

  openFile = (file: File) => {
    this.fileService.open(file)
      .subscribe(event => {
        if (event instanceof HttpResponse) {
          const fileModel = event.body as FileModel;
          this.onFileChange.emit(fileModel);
        }
      });
  }

  selectFile = (id: string) => {
    this.fileService.get(id)
      .subscribe(file => this.onFileChange.emit(file));
  }

  getFiles = () => {
    this.fileService.list(this.getFilesQuery)
      .subscribe(response => this.filesPagedList = response);
  }

  filePinToggle = (id: string) => {
    this.fileService.togglePin(id)
      .subscribe(_ => {
        this.getFilesQuery.page = 1;
        this.getFiles();
      });
  }

  deleteFile = (id: string) => {
    const dialog = this.dialog.open(ConfirmModalComponent, {
      width: '350px',
      backdropClass: 'explicit-overlay-dark-backdrop',
      data: {
        message: 'Do you really want to delete this file?',
        type: ConfirmModalType.Danger,
        confirmMessage: 'Yes, delete',
        cancelMessage: 'No, keep'
      }
    });

    dialog.afterClosed().subscribe(isConfirmed => {
      if (isConfirmed) {
        this.fileService.delete(id).subscribe(_ => this.getFiles());
      }
    });
  }

  downloadFile = (id: string) => {
    const file = this.filesPagedList.items.filter(f => f.id === id)[0];
    this.fileService.download(id).subscribe(data => {
      saveAs(data, file.name + '.idf');
    });
  }

  pageChanged = (page: number) => {
    this.getFilesQuery.page = page;
    this.getFiles();
  }

  isFilesPaginationDisplayed = () => this.filesPagedList.total > this.getFilesQuery.take;

  public dropped = (files: NgxFileDropEntry[]) => {
      if (files.length > 1) {
        alert('Only one file is allowed');
      }
      const fileEntry = files[0].fileEntry as FileSystemFileEntry;
      fileEntry.file((file: File) => {
        this.openFile(file);
      });
  }
}
