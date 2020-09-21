import { environment } from './../../../environments/environment';
import { PlantService } from './../plant.service';
import { Component, OnInit } from '@angular/core';
import { Plant } from '../plant.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-plant-form',
    templateUrl: 'plant-form.component.html'
})

export class PlantFormComponent implements OnInit {

    plant: Plant = new Plant();

    uploadedFile: File;

    slika: string;
    slikaUPrirodi: string;

    baseUrl = environment.baseUrl;

    constructor(
        private plantService: PlantService,
        private activatedRoute: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit() {
        this.activatedRoute.params
            .subscribe(params => {
                const id = +params['id'];
                if (id) {
                    this.plantService.getPlantById(id)
                        .subscribe(response => {
                            this.plant = response;
                            this.slika = this.plant.slika;
                            this.slikaUPrirodi = this.plant.slikaUPrirodi;
                        });
                }
            });
    }

    save() {
        if (this.plant.id) {
            this.plantService.update(this.plant)
                .subscribe(
                    response => this.router.navigateByUrl('/plants'),
                    (error) => {
                        if (error.status == 401) {
                            this.router.navigateByUrl('/plants');
                        } else {
                            alert(error.message);
                        }
                    }
                );
        } else {
            this.plantService.create(this.plant)
            .subscribe(
                response => this.router.navigateByUrl('/plants'),
                (error) => {
                    if (error.status == 401) {
                        alert('You are not logged in')
                        this.router.navigateByUrl('/plants');
                    } else {
                        alert(error.message);
                    }
                }
            );
        }
    }

    imageUploaded(event, type: 'slika' | 'slikaUPrirodi') {
        const targetFile = event.target;

        this.uploadedFile = targetFile.files[0];
        const myReader: FileReader = new FileReader();

        myReader.onloadend = (e) => {
            const slikaBase64 = (myReader.result as string).replace(
                `data:${this.uploadedFile.type};base64,`,
                ''
            );
            if (type === 'slika') {
                this.plant.slikaBase64 = slikaBase64;
                this.plant.slika = this.uploadedFile.name;
            } else if (type === 'slikaUPrirodi') {
                this.plant.slikaUPrirodiBase64 = slikaBase64;
                this.plant.slikaUPrirodi = this.uploadedFile.name;
            }
        };

        myReader.readAsDataURL(this.uploadedFile);
    }

    removeImage(type: 'slika' | 'slikaUPrirodi') {
        this.plantService.deleteImage(this.plant.id, type)
            .subscribe(response => {
                if (type === 'slika') {
                    this.slika = null;
                    this.plant.slika = null;
                    this.plant.slikaBase64 = null;
                } else if (type === 'slikaUPrirodi') {
                    this.slikaUPrirodi = null;
                    this.plant.slikaUPrirodi = null;
                    this.plant.slikaUPrirodiBase64 = null;
                }
            })
    }
}
