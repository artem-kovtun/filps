import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {StorageAuthentication} from '../../models/storageAuthentication.model';
import {ActivatedRoute} from '@angular/router';
import {TranslateService} from '@ngx-translate/core';
import {Storage} from '../../models/enums/storage.enum';
import {File} from '../../models/file.model';
import {UserService} from '../../services/user.service';

@Component({
  selector: 'app-file-selector-page',
  templateUrl: './file-selector-page.component.html',
  styleUrls: ['./file-selector-page.component.scss']
})
export class FileSelectorPageComponent implements OnInit {

  files = [
    {
      name: 'Examination',
      source: Storage.GoogleDrive,
      isPinned: true,
      createdOn: new Date(2021, 3, 23)
    },
    {
      name: 'aws2021practice01-aws-basics-v01',
      source: null,
      isPinned: false,
      createdOn: new Date(2020, 11, 13)
    },
    {
      name: 'Document',
      source: Storage.OneDrive,
      isPinned: false,
      createdOn: new Date(2020, 11, 12)
    }
  ];


  @Output() onFileChange = new EventEmitter<File>();

  Storage = Storage;

  isDataLoaded = false;
  isSelected = false;
  isLoginToApplication = true;
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


  constructor(private route: ActivatedRoute,
              private userService: UserService,
              private translate: TranslateService) {
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

    this.userService.getAuthentication().subscribe(data => {
      this.authData = data.storages;
      this.isDataLoaded = true;
    });
  }

  ngOnInit(): void {
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

  isAuthorized = (storage: Storage) => {
    const storageData = this.authData.filter(ad => ad.storage === storage)[0];
    if (storageData === undefined) return false;
    return storageData.isAuthorized;
  }

  createNewBlankDocument = () => this.onFileChange.emit(new File() );

}
