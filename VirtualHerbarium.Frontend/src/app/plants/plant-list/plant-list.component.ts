import { AuthService } from './../../account/auth.service';
import { PlantService } from './../plant.service';
import { Component, OnInit } from '@angular/core';
import { Plant } from '../plant.model';
import { PlantFilter as PlantFilterParams } from './plant-filter-params.model';

@Component({
    selector: 'app-plant-list',
    templateUrl: 'plant-list.component.html',
    styleUrls: ['plant-list.component.css']
})

export class PlantListComponent implements OnInit {

    plants: Plant[] = [];
    plantFilterParams: PlantFilterParams = new PlantFilterParams();

    public innerWidth: number;

    constructor(private plantService: PlantService, public authService: AuthService) { }

    ngOnInit() {
        this.innerWidth = window.innerWidth;
        console.log(this.innerWidth)
        this.getAllPlants();
    }

    private getAllPlants() {
        this.plantService.getAllPlants(this.plantFilterParams)
            .subscribe(response => this.plants = response);
    }

    search() {
        this.getAllPlants();
    }

    delete(id: number) {
        const confirm = window.confirm('Da li ste sigurni da zelite da izbrisete ovu biljku?');
        if (confirm) {
            this.plantService.delete(id)
                .subscribe(() => this.getAllPlants());
        }
    }
}
