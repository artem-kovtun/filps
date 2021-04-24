import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {UserAuthentication} from '../models/userAuthentication.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAuthentication(): Observable<UserAuthentication> {
    return this.http.get<UserAuthentication>('https://localhost:5001/api/users/authentication', { withCredentials: true });
  }

}
