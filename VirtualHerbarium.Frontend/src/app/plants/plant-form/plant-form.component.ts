import { AuthService } from './../../account/auth.service';
import { environment } from './../../../environments/environment';
import { PlantService } from './../plant.service';
import { Component, OnInit } from '@angular/core';
import { Plant } from '../plant.model';
import { ActivatedRoute, Router } from '@angular/router';
import { PlantImage } from '../plant-image.model';

@Component({
    selector: 'app-plant-form',
    templateUrl: 'plant-form.component.html'
})

export class PlantFormComponent implements OnInit {

    plant: Plant = new Plant();

    uploadedFile: File;

    baseUrl = environment.baseUrl;

    constructor(
        private plantService: PlantService,
        private authService: AuthService,
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
                            alert('Vasa sesija je istekla');
                            this.authService.logout();
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
                        alert('Vasa sesija je istekla');
                        this.authService.logout();
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

            const plantImage: PlantImage = new PlantImage();

            plantImage.slika = this.uploadedFile.name;
            plantImage.slikaBase64 = slikaBase64;
            plantImage.biljkaId = this.plant.id;

            if (type === 'slika') {
                plantImage.uPrirodi = false;
                this.plant.slike.push(plantImage);
            } else if (type === 'slikaUPrirodi') {
                plantImage.uPrirodi = true;
                this.plant.slikeUPrirodi.push(plantImage);
            }
        };

        myReader.readAsDataURL(this.uploadedFile);
    }

    removeImage(index: number, type: 'slika' | 'slikaUPrirodi') {
        if (type === 'slika') {
            this.plant.slike.splice(index, 1);
        } else {
            this.plant.slikeUPrirodi.splice(index, 1);
        }
    }
}
