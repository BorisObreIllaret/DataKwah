import {NgModule} from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from "@angular/forms";
import {HttpClientModule} from '@angular/common/http';

import {ButtonModule} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {DialogModule} from 'primeng/dialog';
import {InputTextModule} from 'primeng/inputtext';
import {MessageService} from 'primeng/api';
import {RatingModule} from 'primeng/rating';
import {RippleModule} from 'primeng/ripple';
import {TableModule} from 'primeng/table';
import {InputTextareaModule} from 'primeng/inputtextarea';
import {ToastModule} from 'primeng/toast';
import {ToolbarModule} from 'primeng/toolbar';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {LayoutComponent} from './layout/layout.component';
import {HomeComponent} from './home/home.component';
import {ToStateIconPipe} from './pipes/to-state-icon.pipe';
import {ProductDetailsComponent} from './product-details/product-details.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    HomeComponent,
    ToStateIconPipe,
    ProductDetailsComponent
  ],
  imports: [
    AppRoutingModule,
    ButtonModule,
    BrowserAnimationsModule,
    BrowserModule,
    CardModule,
    DialogModule,
    FormsModule,
    HttpClientModule,
    InputTextModule,
    TableModule,
    RatingModule,
    RippleModule,
    InputTextareaModule,
    ToastModule,
    ToolbarModule,
  ],
  providers: [
    MessageService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
