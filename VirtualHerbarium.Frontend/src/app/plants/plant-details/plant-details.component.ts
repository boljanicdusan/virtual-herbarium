import { environment } from './../../../environments/environment';
import { Component, OnInit } from '@angular/core';
import { Plant } from '../plant.model';
import { Router, ActivatedRoute } from '@angular/router';
import { PlantService } from '../plant.service';

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
              const image = {
                image: this.baseUrl + 'images/' + this.plant.slika,
                thumbImage: this.baseUrl + 'images/' + this.plant.slika,
                alt: '',
                title: this.plant.vrsta,
              };
              this.imagesObject.push(image);

            });
        }
      });
  }

  showLightbox(index) {
    this.selectedImageIndex = index;
    this.showFlag = true;
  }

  closeEventHandler() {
    this.showFlag = false;

  }
}
