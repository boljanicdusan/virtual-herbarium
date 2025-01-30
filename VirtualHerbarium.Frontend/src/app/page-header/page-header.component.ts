import { Component, OnInit } from '@angular/core';
import { AuthService } from '../account/auth.service';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-page-header',
    templateUrl: 'page-header.component.html',
    styleUrls: ['page-header.component.css']
})

export class PageHeaderComponent implements OnInit {

    innerWidth: number;
    hiddenForMobile = false;
    hiddenForTablet = false;

    constructor(public authService: AuthService) { }

    ngOnInit() {
        this.setScreenWidthSettings();
    }

    private setScreenWidthSettings() {
        this.innerWidth = window.innerWidth;
        this.hiddenForMobile = this.innerWidth < 600;
        this.hiddenForTablet = this.innerWidth < 900;
    }
}
