import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { en_US } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import {NzIconModule} from 'ng-zorro-antd/icon';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzTagModule } from 'ng-zorro-antd/tag';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import { FolderComponent } from './components/folder/folder.component';
import { FoldersComponent } from './components/folders/folders.component';
import { DocumentsComponent } from './components/documents/documents.component';
import { StorageHeaderComponent } from './components/storage-header/storage-header.component';
import { NoFilesComponent } from './components/no-files/no-files.component';
import { StorageComponent } from './components/storage/storage.component';
import { StorageAuthComponent } from './components/storage-auth/storage-auth.component';
import {QuestionAnswerComponent} from './components/question-answer/question-answer.component';
import {YoutubeEmbeddedComponent} from './components/youtube-embedded/youtube-embedded.component';
import {HeadingComponent} from './components/heading/heading.component';
import {DocumentElementComponent} from './components/document-element/document-element.component';
import {TextComponent} from './components/text/text.component';
import {QuestionOptionsComponent} from './components/question-options/question-options.component';
import {ImagesCollageComponent} from './components/images-collage/images-collage.component';
import {FillMissingComponent} from './components/fill-missing/fill-missing.component';
import {AudioPlayerComponent} from './components/audio-player/audio-player.component';
import {QuillModule} from 'ngx-quill';
import {NgxAudioPlayerModule} from 'ngx-audio-player';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';
import {MatMenuModule} from '@angular/material/menu';
import {DragDropModule} from '@angular/cdk/drag-drop';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzListModule } from 'ng-zorro-antd/list';
import { MatDialogModule } from '@angular/material/dialog';
import { NzBadgeModule } from 'ng-zorro-antd/badge';
import {SectionPickComponent} from './components/section-pick/section-pick.component';
import { FileComponent } from './components/file/file.component';
import { FilePageComponent } from './components/file-page/file-page.component';
import { FileSelectorPageComponent } from './components/file-selector-page/file-selector-page.component';
import { HeaderComponent } from './components/header/header.component';
import { ContentComponent } from './components/content/content.component';
import { FileLocationPickerModalComponent } from './components/file-location-picker-modal/file-location-picker-modal.component';
import { ConfirmModalComponent } from './components/confirm-modal/confirm-modal.component';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import {SocialLoginModule, SocialAuthServiceConfig, FacebookLoginProvider} from 'angularx-social-login';
import { GoogleLoginProvider } from 'angularx-social-login';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { MomentModule } from 'ngx-moment';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { DocumentRecordComponent } from './components/document-record/document-record.component';
import {AuthTokenInterceptor} from './interceptors/authToken.interceptor';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import {NgxFileDropModule} from 'ngx-file-drop';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
registerLocaleData(en);

@NgModule({
  declarations: [
    AppComponent,
    FolderComponent,
    FoldersComponent,
    DocumentsComponent,
    StorageHeaderComponent,
    NoFilesComponent,
    StorageComponent,
    StorageAuthComponent,
    QuestionAnswerComponent,
    QuestionOptionsComponent,
    YoutubeEmbeddedComponent,
    HeadingComponent,
    DocumentElementComponent,
    TextComponent,
    ImagesCollageComponent,
    FillMissingComponent,
    AudioPlayerComponent,
    SectionPickComponent,
    FileComponent,
    FilePageComponent,
    FileSelectorPageComponent,
    HeaderComponent,
    ContentComponent,
    FileLocationPickerModalComponent,
    ConfirmModalComponent,
    DocumentRecordComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        FormsModule,
        HttpClientModule,
        BrowserAnimationsModule,
        NzIconModule,
        NzTabsModule,
        NzBreadCrumbModule,
        NzSpinModule,
        NzCheckboxModule,
        NzToolTipModule,
        NzSelectModule,
        NzDropDownModule,
        HttpClientModule,
        NzCarouselModule,
        MatMenuModule,
        DragDropModule,
        NzInputModule,
        NzRadioModule,
        NzSwitchModule,
        NzListModule,
        MatDialogModule,
        NzBadgeModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [HttpClient]
            }
        }),
        QuillModule.forRoot({
            modules: {
                toolbar: [
                    ['bold', 'italic', 'underline'],
                    ['blockquote'],
                    [{list: 'ordered'}, {list: 'bullet'}, {align: []}],
                    [{header: [1, 2, 3, 4, 5, 6, false]}],
                    [{color: []}, {background: []}],
                    ['clean']
                ]
            }
        }),
        ReactiveFormsModule,
        NgxAudioPlayerModule,
        SocialLoginModule,
        NzAvatarModule,
        NzPaginationModule,
        MomentModule,
        NzTagModule,
        NzButtonModule,
        NzDividerModule,
        NgxFileDropModule,
        NzDatePickerModule,

    ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthTokenInterceptor, multi: true },
    { provide: NZ_I18N, useValue: en_US },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '821014846315-qkpfbrbhitqa8m6bn39kbe0ujfllkuvs.apps.googleusercontent.com'
            )
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('4287975794607036')
          }
        ]
      } as SocialAuthServiceConfig,
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}
