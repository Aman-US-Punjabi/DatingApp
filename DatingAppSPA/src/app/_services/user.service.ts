import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  apiUrl = environment.apiUrl;
  userApiUrl = this.apiUrl + 'users/';

  constructor(
    private http: HttpClient
  ) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.userApiUrl);
  }

  getUser(userId: number): Observable<User> {
    return this.http.get<User>(this.userApiUrl + `${userId}`);
  }

  updateUser(id: number, user: User) {
    return this.http.put(this.userApiUrl + id, user);
  }

  setMainPhoto(userId: number, id: number) {
    return this.http.post(this.userApiUrl + userId + '/photos/' + id + '/setMain', {});
  }

  deletePhoto(userId: number, id: number) {
    return this.http.delete(this.userApiUrl + userId + '/photos/' + id);
  }
}
