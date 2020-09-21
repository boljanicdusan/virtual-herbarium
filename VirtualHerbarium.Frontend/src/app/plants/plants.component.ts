import { Component, OnInit } from '@angular/core';
import { AuthService } from '../account/auth.service';

@Component({
    selector: 'app-plants',
    templateUrl: 'plants.component.html',
    styleUrls: ['plants.component.css']
})

export class PlantsComponent implements OnInit {

    constructor(public authService: AuthService) { }

    ngOnInit() { }
}
