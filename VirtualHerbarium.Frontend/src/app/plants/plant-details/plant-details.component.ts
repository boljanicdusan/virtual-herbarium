import { environment } from './../../../environments/environment';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Plant } from '../plant.model';
import { ActivatedRoute } from '@angular/router';
import { PlantService } from '../plant.service';
import { Image } from './image.model';
import { GoogleMap, MapInfoWindow, MapMarker } from '@angular/google-maps';
import { PlantImage } from '../plant-image.model';

declare const google: any;

@Component({
    selector: 'app-plant-details',
    templateUrl: 'plant-details.component.html',
    styleUrls: ['plant-details.component.css']
})

export class PlantDetailsComponent implements OnInit {

    @ViewChild(MapInfoWindow, { static: false }) infoWindow: MapInfoWindow;
    @ViewChild('map', { static: false }) map: GoogleMap;

    plant: Plant = new Plant();
    slike: PlantImage[] = [];

    showFlag = false;
    selectedImageIndex = -1;
    baseUrl = environment.baseUrl;
    imagesObject = [];

    zoom = 8.5;
    center: google.maps.LatLngLiteral;
    // marker: google.maps.LatLngLiteral;
    markers: google.maps.LatLngLiteral[] = [];
    options: google.maps.MapOptions = {
        // mapTypeId: 'satellite',
        mapTypeId: 'terrain',
        zoomControl: true,
        scrollwheel: true,
        disableDoubleClickZoom: false,
        maxZoom: 25,
        minZoom: 8,
        // tilt: 45,
        styles: [
            {
              featureType: 'poi',
              stylers: [{ visibility: 'off' }],
            },
          ],
    };


    constructor(private plantService: PlantService, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.scrollToTheTop();

        this.activatedRoute.params
            .subscribe(params => {
                const id = +params['id'];
                if (id) {
                    this.plantService.getPlantById(id)
                        .subscribe(response => {
                            this.plant = response;

                            this.slike = this.plant.slike.concat(this.plant.slikeUPrirodi);
                            this.imagesObject = this.slike.map(s => this.createImageObject(s.slika));

                            this.plant.lokacijeBiljaka.forEach(l => {
                                if (l.latitude && l.longitude) {
                                    const marker = {
                                        lat: l.latitude,
                                        lng: l.longitude
                                    };
                                    this.markers.push(marker);
                                }
                            });

                            this.center = {
                                lat: 42.8098673841108,
                                lng: 19.35
                            };
                        });
                }
            });

        setTimeout(() => {
            this.map.data.loadGeoJson(
                '../../assets/files/MNE_utm_10x10k_geoJSON_converted.geojson',
                null,
                (features) => {}
            );
            this.map.data.setStyle({
                strokeWeight: 1,
                strokeOpacity: 0.5,
                fillOpacity: 0.01
              });
        }, 1000);

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
        this.selectedImageIndex = index;
        this.showFlag = true;
    }

    closeEventHandler() {
        this.showFlag = false;
    }

    openInfo(marker: MapMarker) {
        this.infoWindow.open(marker);
    }

    scrollToTheTop() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    }
}
