import { NgModule } from '@angular/core';
import {
  BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule, ModalModule
} from 'ngx-bootstrap';

@NgModule({
  imports: [
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ButtonsModule.forRoot(),
    ModalModule.forRoot()
  ],
  exports: [
    BsDropdownModule,
    TabsModule,
    BsDatepickerModule,
    PaginationModule,
    ButtonsModule,
    ModalModule
  ],
  declarations: []
})
export class BootstrapModule { }
