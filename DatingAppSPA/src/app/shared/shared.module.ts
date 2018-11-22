import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BootstrapModule } from '../bootstrap/bootstrap.module';

@NgModule({
  imports: [
    CommonModule,
    BootstrapModule
  ],
  exports: [
    CommonModule,
    BootstrapModule
  ],
  declarations: []
})
export class SharedModule { }
