import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/User';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/';
  currentUser = signal<User | null>(null); // User ou nulo

  /**
   * Faz o login na API e salva o usuario no local storage
   * e no signal currentUser.
   * @param model objeto com username e password
   * @returns observable que emite um usuario se o login for valido
   */
  login(model: any) {
    // do return a o .pipe() para retornar um observable	
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      // pega a resposta e transforma em signal
      map(user => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    );
  }

  register(model: any) {
    // do return a o .pipe() para retornar um observable	
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      // pega a resposta e transforma em signal
      map(user => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      })
    );
  }


  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
