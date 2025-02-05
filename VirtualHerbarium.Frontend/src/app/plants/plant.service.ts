import { Plant } from './plant.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PlantService {

    url: string = environment.baseUrl + 'api/plants/';

    constructor(private http: HttpClient) { }

    getAllPlants(params: any): Observable<Plant[]> {
        return this.http.get<Plant[]>(this.url, { params, headers: this.createAuthHeaders() });
    }

    getPlantById(id): Observable<Plant> {
        return this.http.get<Plant>(this.url + id, { headers: this.createAuthHeaders() });
    }

    create(plant: Plant): Observable<Plant> {
        return this.http.post<Plant>(this.url, plant, { headers: this.createAuthHeaders() });
    }

    update(plant: Plant): Observable<Plant> {
        return this.http.put<Plant>(this.url + plant.id, plant, { headers: this.createAuthHeaders() });
    }

    delete(id: number): Observable<any> {
        return this.http.delete(this.url + id, { headers: this.createAuthHeaders() });
    }

    deleteImage(id: number, type: string): Observable<any> {
        return this.http.delete(this.url + 'deleteimage/' + id, { headers: this.createAuthHeaders(), params: { type }});
    }

    private createAuthHeaders(): HttpHeaders {
        const token = localStorage.getItem('token');
        const headers = new HttpHeaders();
        return headers.set('Authorization', `Bearer ${token}`);
    }
}
