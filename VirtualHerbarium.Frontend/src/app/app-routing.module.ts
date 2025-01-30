import { AuthGuardService } from './auth-guard.service';
import { PlantsComponent } from './plants/plants.component';
import { LoginComponent } from './account/login/login.component';
import { PlantDetailsComponent } from './plants/plant-details/plant-details.component';
import { PlantFormComponent } from './plants/plant-form/plant-form.component';
import { PlantListComponent } from './plants/plant-list/plant-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main/main.component';
import { AboutUsComponent } from './about-us/about-us.component';

const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: '',
        component: MainComponent,
        children: [
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
                path: 'about-us',
                component: AboutUsComponent
            },
            {
                path: '**',
                redirectTo: 'plants'
            }
        ]
    },
    {
        path: '**',
        redirectTo: 'plants'
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
