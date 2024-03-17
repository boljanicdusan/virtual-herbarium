import { Component, OnInit } from '@angular/core';
import { AuthService } from '../account/auth.service';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-plants',
    templateUrl: 'plants.component.html',
    styleUrls: ['plants.component.css']
})

export class PlantsComponent implements OnInit {

    baseUrl = environment.baseUrl;

    constructor(public authService: AuthService) { }

    ngOnInit() { }
}
