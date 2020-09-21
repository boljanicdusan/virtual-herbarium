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

    plant: Plant = new Plant();

    baseUrl = environment.baseUrl;

    constructor(private plantService: PlantService, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.activatedRoute.params
            .subscribe(params => {
                const id = +params['id'];
                if (id) {
                    this.plantService.getPlantById(id)
                        .subscribe(response => this.plant = response);
                }
            });
    }
}
