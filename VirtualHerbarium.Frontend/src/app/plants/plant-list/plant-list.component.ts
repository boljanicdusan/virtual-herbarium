import { AuthService } from './../../account/auth.service';
import { PlantService } from './../plant.service';
import { Component, OnInit } from '@angular/core';
import { Plant } from '../plant.model';
import { PlantFilter as PlantFilterParams } from './plant-filter-params.model';
import { finalize } from 'rxjs/operators';
@Component({
    selector: 'app-plant-list',
    templateUrl: 'plant-list.component.html',
    styleUrls: ['plant-list.component.css']
})

export class PlantListComponent implements OnInit {

    plants: Plant[] = [];
    plantFilterParams: PlantFilterParams = new PlantFilterParams();

    innerWidth: number;
    hiddenForMobile = false;
    hiddenForTablet = false;

    isLoading = false;

    constructor(private plantService: PlantService, public authService: AuthService) { }

    ngOnInit() {
        this.setScreenWidthSettings();
        this.getAllPlants();
    }

    private setScreenWidthSettings() {
        this.innerWidth = window.innerWidth;
        this.hiddenForMobile = this.innerWidth < 600;
        this.hiddenForTablet = this.innerWidth < 900;
    }

    private getAllPlants() {
        this.isLoading = true;
        this.plantService.getAllPlants(this.plantFilterParams)
            .pipe(finalize(() => this.isLoading = false))
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

