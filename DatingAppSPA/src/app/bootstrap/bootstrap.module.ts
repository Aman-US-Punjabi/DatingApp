import { NgModule } from '@angular/core';
import {
  BsDropdownModule, TabsModule
} from 'ngx-bootstrap';

@NgModule({
  imports: [
    BsDropdownModule.forRoot(),
    TabsModule.forRoot()
  ],
  exports: [
    BsDropdownModule,
    TabsModule
  ],
  declarations: []
})
export class BootstrapModule { }
