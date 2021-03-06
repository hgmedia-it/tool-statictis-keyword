import { BrowserModule } from '@angular/platform-browser';
import { Component, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CampaignComponent } from './data/campaign/campaign.component';
import { KeywordComponent } from './data/keyword/keyword.component';
import { VideoComponent } from './data/video/video.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CampaignComponent,
    KeywordComponent,
    VideoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'data/campaign', component: CampaignComponent, pathMatch: 'full', canActivate: [AuthorizeGuard]},
    {path: 'data/keyword',component: KeywordComponent, pathMatch:'full', canActivate:[AuthorizeGuard]},
    {path: 'data/video',component: VideoComponent, pathMatch:'full', canActivate:[AuthorizeGuard]}
], { relativeLinkResolution: 'legacy' })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
