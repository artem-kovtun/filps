import {Component, Input, OnInit} from '@angular/core';
import {StorageData} from '../../models/storageData.model';
import {Storage} from '../../models/enums/storage.enum';
import {GetFilesQuery} from '../../models/getFilesQuery.model';
import {FileMetadata} from '../../models/fileMetadata.model';
import {FileService} from '../../services/file.service';

@Component({
  selector: 'app-storage',
  templateUrl: './storage.component.html',
  styleUrls: ['./storage.component.scss']
})
export class StorageComponent implements OnInit {

  @Input() storage: Storage;
  @Input() isAuthorized: boolean;
  data: StorageData = new StorageData();
  query: GetFilesQuery = new GetFilesQuery(1, 10);
  breadcrumb: Array<FileMetadata> = [];
  syncing = false;

  isDataPresent: boolean;
  isDataLoading = true;

  constructor(private fileService: FileService) { }

  ngOnInit(): void {
    /*
        if (this.isAuthorized) {
      this.fileService.list(this.query).subscribe(data => {
        this.data = data;
        this.isDataPresent = !(data.folders.length === 0 && data.files.length === 0);
        this.isDataLoading = false;
      });
    }
     */

  }

  sync = () => {
    this.fileService.list(this.query).subscribe(data => {
      /*
      this.data = data;
      this.isDataPresent = !(data.folders.length === 0 && data.files.length === 0);
       */
      this.isDataLoading = false;
    });
  }

  fileSelected = (id: string) => {
    alert(`File with id = ${id} has been selected`);
  }

  folderSelected = (id: string) => {
    /*
    this.isDataLoading = true;
    const folder = this.data.folders.filter(f => f.id === id)[0];
    this.query.parentId = id;
    this.fileService.list(this.query).subscribe(data => {
      this.isDataPresent = !(data.folders.length === 0 && data.files.length === 0);
      this.data = data;
      this.isDataLoading = false;
      if (folder !== undefined){
        this.breadcrumb.push(folder);
      }
    });
     */

  }

  search = (query: string) => {
    /*
        this.isDataLoading = true;
    this.query.search = query;
    this.fileService.list(this.query).subscribe(data => {
      this.data = data;
      this.isDataPresent = !(data.folders.length === 0 && data.files.length === 0);
      this.isDataLoading = false;
    });
     */
  }

  backClick = () => {
    /*
    this.isDataLoading = true;
    const breadcrumbLength = this.breadcrumb.length;
    this.query.parentId = breadcrumbLength > 1 ? this.breadcrumb[breadcrumbLength - 2].id : null;
    this.fileService.list(this.query).subscribe(data => {
      this.isDataPresent = !(data.folders.length === 0 && data.files.length === 0);
      this.data = data;
      this.breadcrumb.pop();
      this.isDataLoading = false;
    });
   */
  }

  getData = () => {
    /*
    this.isDataLoading = true;
    this.fileService.list(this.query).subscribe(data => {
      this.data = data;
      this.isAuthorized = true;
      this.isDataPresent = !(data.folders.length === 0 && data.files.length === 0);
      this.isDataLoading = false;
    });
 */
  }

  syncData = () => {
    /*
    this.isDataLoading = true;
    this.fileService.list(this.query).subscribe(data => {
      this.data = data;
      this.isDataPresent = !(data.folders.length === 0 && data.files.length === 0);
      this.isDataLoading = false;
    });
 */
  }
}
