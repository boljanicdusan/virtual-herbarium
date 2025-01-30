import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'app-carousel',
    templateUrl: './carousel.component.html',
    styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {

    @Input() images: string[] = [];  // Array of image URLs
    @Input() autoSlide: boolean = true; // Enable auto-slide
    @Input() slideInterval: number = 3000; // Slide every 3 seconds

    currentIndex: number = 0;
    intervalId: any;

    ngOnInit() {
        if (this.autoSlide) {
            this.startAutoSlide();
        }
    }

    startAutoSlide() {
        this.intervalId = setInterval(() => {
            this.next();
        }, this.slideInterval);
    }

    stopAutoSlide() {
        if (this.intervalId) {
            clearInterval(this.intervalId);
        }
    }

    prev() {
        this.currentIndex = this.currentIndex === 0 ? this.images.length - 1 : this.currentIndex - 1;
    }

    next() {
        this.currentIndex = (this.currentIndex + 1) % this.images.length;
    }

    // Go to a specific slide
    goToSlide(index: number) {
        this.currentIndex = index;
    }
}
