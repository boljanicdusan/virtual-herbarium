import { environment } from './../../../environments/environment';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Plant } from '../plant.model';
import { Router, ActivatedRoute } from '@angular/router';
import { PlantService } from '../plant.service';
export interface Image {
  image: string;
  thumbImage: string;
  alt: string;
  title: string;
}
@Component({
  selector: 'app-plant-details',
  templateUrl: 'plant-details.component.html'
})

export class PlantDetailsComponent implements OnInit {
  showFlag = false;
  selectedImageIndex = -1;
  plant: Plant = new Plant();
  baseUrl = environment.baseUrl;
  imagesObject = [];


  constructor(private plantService: PlantService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {

    this.activatedRoute.params
      .subscribe(params => {
        const id = +params['id'];
        if (id) {
          this.plantService.getPlantById(id)
            .subscribe(response => {
              this.plant = response;
            });
        }
      });
  }

  createImageObject(slika: string) {
    const image: Image = {
      image: this.baseUrl + 'images/' + slika,
      thumbImage: this.baseUrl + 'images/' + slika,
      alt: '',
      title: this.plant.vrsta,
    };
    return image;

  }

  showLightboxSlika(index, parameter: string) {
    this.imagesObject = [];
    if (parameter === 'slika') {
    this.imagesObject.push(this.createImageObject(this.plant.slika));
    } else {
    this.imagesObject.push(this.createImageObject(this.plant.slikaUPrirodi));
    }
    this.selectedImageIndex = index;
    this.showFlag = true;
  }

  closeEventHandler() {
    this.showFlag = false;
  }
}
