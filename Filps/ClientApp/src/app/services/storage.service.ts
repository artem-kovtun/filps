import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {StorageData} from '../models/storageData.model';
import {Observable} from 'rxjs';
import {GetFilesQuery} from '../models/getFilesQuery.model';
import {UserAuthentication} from '../models/userAuthentication.model';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private http: HttpClient) { }

  list(query: GetFilesQuery): Observable<StorageData> {
    return this.http.post<StorageData>('https://localhost:5001/api/files/list', query, { withCredentials: true });
  }

}
