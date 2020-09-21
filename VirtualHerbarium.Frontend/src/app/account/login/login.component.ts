import { Router } from '@angular/router';
import { AuthService } from './../auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../user.model';

@Component({
    selector: 'app-login',
    templateUrl: 'login.component.html',
    styleUrls: ['login.component.css']
})

export class LoginComponent implements OnInit {

    user: User = new User();

    constructor(private authService: AuthService, private router: Router) { }

    ngOnInit() { }

    login() {
        this.authService.login(this.user)
            .subscribe(
                response => {
                    if (response.token) {
                        localStorage.setItem('token', response.token);
                        this.router.navigateByUrl('/plants');
                    } else {
                        alert('Login failed');
                    }
                },
                () => alert('Login failed')
            );
    }
}
