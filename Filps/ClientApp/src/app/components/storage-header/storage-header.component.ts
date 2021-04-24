import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {StorageObject} from '../../models/storageObject.model';
import {FormControl} from '@angular/forms';
import { debounceTime, distinctUntilChanged, takeWhile } from 'rxjs/operators';

@Component({
  selector: 'app-storage-header',
  templateUrl: './storage-header.component.html',
  styleUrls: ['./storage-header.component.scss']
})
export class StorageHeaderComponent implements OnInit {

  @ViewChild('searchElement') searchElement: ElementRef;

  @Input() breadcrumb: Array<StorageObject> = [];
  @Output() backClick = new EventEmitter();
  @Output() syncClick = new EventEmitter();
  @Output() onSearch = new EventEmitter<string>();

  @Input() syncing = false;
  isSearching = false;
  searchQuery: string;
  search = new FormControl();

  constructor() { }

  ngOnInit(): void {
    this.search.valueChanges.pipe(
      debounceTime(500),
      distinctUntilChanged())
      .subscribe(data => {
        if (data !== undefined) {
          this.onSearch.emit(data);
        }
      });
  }

  showSearch = () => {
    this.isSearching = true;
    setTimeout(() => this.searchElement.nativeElement.focus(), 0);
  }

  hideSearch = () => {
    this.searchQuery = null;
    this.isSearching = false;
  }

  sync = () => this.syncClick.emit();

  isBackButtonVisible = () => this.breadcrumb.length !== 0;

  back = () => this.backClick.emit();

}
