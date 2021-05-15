import { environment } from './../../../environments/environment';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Plant } from '../plant.model';
import { ActivatedRoute } from '@angular/router';
import { PlantService } from '../plant.service';
import { Image } from './image.model';
import { MapInfoWindow, MapMarker } from '@angular/google-maps';

declare const google: any;

@Component({
    selector: 'app-plant-details',
    templateUrl: 'plant-details.component.html'
})

export class PlantDetailsComponent implements OnInit {

    @ViewChild(MapInfoWindow, { static: false }) infoWindow: MapInfoWindow;

    showFlag = false;
    selectedImageIndex = -1;
    plant: Plant = new Plant();
    baseUrl = environment.baseUrl;
    imagesObject = [];

    zoom = 15;
    center: google.maps.LatLngLiteral;
    marker: google.maps.LatLngLiteral;
    options: google.maps.MapOptions = {
        // mapTypeId: 'satellite',
        mapTypeId: 'hybrid',
        zoomControl: true,
        scrollwheel: true,
        disableDoubleClickZoom: false,
        maxZoom: 25,
        minZoom: 8,
        tilt: 45,
        styles: [
            {
              featureType: 'poi',
              stylers: [{ visibility: 'off' }],
            },
          ],
    };


    constructor(private plantService: PlantService, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {

        this.activatedRoute.params
            .subscribe(params => {
                const id = +params['id'];
                if (id) {
                    this.plantService.getPlantById(id)
                        .subscribe(response => {
                            this.plant = response;
                            if (this.plant.latitude && this.plant.longitude) {
                                this.center = this.marker = {
                                    lat: this.plant.latitude,
                                    lng: this.plant.longitude
                                };
                            } else {
                                this.center = {
                                    lat: 42.4304,
                                    lng: 19.2594
                                }
                            }
                        });
                }
            });

    }

    createImageObject(slika: string) {
        const image: Image = {
            image: this.baseUrl + 'images/' + slika,
            thumbImage: this.baseUrl + 'images/' + slika,
            alt: this.plant.vrsta,
            title: this.plant.vrsta,
        };
        return image;

    }

    showLightboxSlika(index, parameter: string) {
        this.imagesObject = [];
        if (parameter === 'slika') {
            this.imagesObject.push(this.createImageObject(this.plant.slike[index].slika));
        } else {
            this.imagesObject.push(this.createImageObject(this.plant.slikeUPrirodi[index].slika));
        }
        this.selectedImageIndex = index;
        this.showFlag = true;
    }

    closeEventHandler() {
        this.showFlag = false;
    }

    openInfo(marker: MapMarker) {
        this.infoWindow.open(marker);
    }
}
