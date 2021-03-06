import { PlantImage } from './plant-image.model';
export class Plant {
    id: number;
    vrsta: string;
    porodica: string;
    red: string;
    trivijalniNaziv: string;
    sinonim: string;
    staniste: string;
    mjesto: string;
    opis: string;
    // slika: string;
    // slikaBase64: string;
    // slikaUPrirodi: string;
    // slikaUPrirodiBase64: string;

    slike: PlantImage[] = [];
    slikeUPrirodi: PlantImage[] = [];

    latitude: number;
    longitude: number;
}
