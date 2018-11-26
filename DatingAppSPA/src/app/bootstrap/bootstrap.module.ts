import { NgModule } from '@angular/core';
import {
  BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule
} from 'ngx-bootstrap';

@NgModule({
  imports: [
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ButtonsModule.forRoot()
  ],
  exports: [
    BsDropdownModule,
    TabsModule,
    BsDatepickerModule,
    PaginationModule,
    ButtonsModule
  ],
  declarations: []
})
export class BootstrapModule { }
