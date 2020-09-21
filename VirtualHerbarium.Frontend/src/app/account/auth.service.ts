import { Router } from '@angular/router';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class AuthService {

    baseUrl: string = environment.baseUrl;

    constructor(private http: HttpClient, private router: Router) { }

    login(data) {
        return this.http.post<any>(this.baseUrl + 'api/auth/', data);
    }

    logout() {
        localStorage.removeItem('token');
        this.router.navigateByUrl('/plants');
    }

    isAuthenticated(): boolean {
        const token = localStorage.getItem('token');
        return token ? true : false;
    }
}
