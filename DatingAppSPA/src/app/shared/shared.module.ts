import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BootstrapModule } from '../bootstrap/bootstrap.module';
import { NgxGalleryModule } from 'ngx-gallery';

@NgModule({
  imports: [
    CommonModule,
    BootstrapModule,
    NgxGalleryModule
  ],
  exports: [
    CommonModule,
    BootstrapModule,
    NgxGalleryModule
  ],
  declarations: []
})
export class SharedModule { }
