import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-about-us',
    templateUrl: 'about-us.component.html',
    styleUrls: ['about-us.component.css']
})

export class AboutUsComponent implements OnInit {

    carouselImages: string[] = [
        'assets/images/1.jpg',
        'assets/images/2.jpg',
        'assets/images/3.jpg',
        'assets/images/4.jpg',
        'assets/images/5.JPG',
        'assets/images/6.jpg',
        'assets/images/7.JPG',
        'assets/images/8.jpg',
        'assets/images/9.JPG',
        'assets/images/10.JPG',
        'assets/images/11.jpg',
        'assets/images/12.jpg',
        'assets/images/13.jpg',
        'assets/images/14.JPG',
        'assets/images/15.jpg',
    ];

    constructor() { }

    ngOnInit() { }
}
