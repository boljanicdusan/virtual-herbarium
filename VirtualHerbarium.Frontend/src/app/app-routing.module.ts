import { AuthGuardService } from './auth-guard.service';
import { PlantsComponent } from './plants/plants.component';
import { LoginComponent } from './account/login/login.component';
import { PlantDetailsComponent } from './plants/plant-details/plant-details.component';
import { PlantFormComponent } from './plants/plant-form/plant-form.component';
import { PlantListComponent } from './plants/plant-list/plant-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    {
        path: 'plants',
        component: PlantsComponent,
        children: [
            {
                path: '',
                component: PlantListComponent
            },
            {
                path: 'new',
                component: PlantFormComponent,
                canActivate: [AuthGuardService]
            },
            {
                path: 'edit/:id',
                component: PlantFormComponent,
                canActivate: [AuthGuardService]
            },
            {
                path: 'details/:id',
                component: PlantDetailsComponent
            },
        ]
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: '**',
        redirectTo: 'plants'
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
