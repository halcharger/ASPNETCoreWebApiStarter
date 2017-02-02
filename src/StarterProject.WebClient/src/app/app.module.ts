import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';

import { requestOptionsProvider } from './default-request-options.service';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-route.module';
import { AppFeaturesModule } from './app-features.module';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        JsonpModule,
        AppRoutingModule,
        AppFeaturesModule,
        NgbModule.forRoot()
    ],
    providers: [requestOptionsProvider],
    bootstrap: [AppComponent]
})
export class AppModule { }
