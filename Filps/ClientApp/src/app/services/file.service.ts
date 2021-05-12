import {Injectable} from '@angular/core';
import {HttpClient, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GetFilesQuery} from '../models/getFilesQuery.model';
import {FileModel} from '../models/file.model';
import {FileMetadata} from '../models/fileMetadata.model';
import {PagedList} from '../models/pagedList.model';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private http: HttpClient) { }

  list(query: GetFilesQuery): Observable<PagedList<FileMetadata>> {
    return this.http.post<PagedList<FileMetadata>>('https://localhost:5001/api/files/list', query, { withCredentials: true });
  }

  save(file: FileModel): Observable<{ id: string }> {
    return this.http.post<{ id: string }>('https://localhost:5001/api/files/save', file, { withCredentials: true });
  }

  get(id: string): Observable<FileModel> {
    return this.http.post<FileModel>('https://localhost:5001/api/files/get', {fileId: id}, { withCredentials: true });
  }

  download(id: string): Observable<Blob> {
    return this.http.post('https://localhost:5001/api/files/download', {fileId: id}, {
      responseType: 'blob',
      withCredentials: true
    });
  }

  togglePin(id: string): Observable<any> {
    return this.http.post('https://localhost:5001/api/files/togglePin', {fileId: id}, { withCredentials: true });
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`https://localhost:5001/api/files?id=${id}`, { withCredentials: true });
  }

  open = (file: File) => {
    const form = new FormData();
    form.append('file', file);

    const request = new HttpRequest('POST', 'https://localhost:5001/api/files/open', form, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(request);
  }

}
