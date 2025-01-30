import { AuthGuardService } from './auth-guard.service';
import { PlantsComponent } from './plants/plants.component';
import { LoginComponent } from './account/login/login.component';
import { AuthService } from './account/auth.service';
import { PlantService } from './plants/plant.service';
import { PlantDetailsComponent } from './plants/plant-details/plant-details.component';
import { PlantFormComponent } from './plants/plant-form/plant-form.component';
import { PlantListComponent } from './plants/plant-list/plant-list.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgImageFullscreenViewModule } from 'ng-image-fullscreen-view';
import { GoogleMapsModule } from '@angular/google-maps'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PageHeaderComponent } from './page-header/page-header.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { MainComponent } from './main/main.component';
import { DisableRightClickDirective } from './directives/disable-right-click.directive';
import { DisableDragDirective } from './directives/disable-drag.directive';
import { CarouselComponent } from './carousel/carousel.component';
import { DisableLongPressDirective } from './directives/disable-long-press.directive';

@NgModule({
  declarations: [
    AppComponent,
    PlantsComponent,
    PlantListComponent,
    PlantFormComponent,
    PlantDetailsComponent,
    LoginComponent,
    PageHeaderComponent,
    AboutUsComponent,
    MainComponent,
    CarouselComponent,
    DisableRightClickDirective,
    DisableDragDirective,
    DisableLongPressDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgImageFullscreenViewModule,
    GoogleMapsModule,
  ],
  providers: [PlantService, AuthService, AuthGuardService],
  bootstrap: [AppComponent]
})
export class AppModule { }
