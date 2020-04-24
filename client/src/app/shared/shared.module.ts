import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';



@NgModule({
  declarations: [PagingHeaderComponent, PagerComponent, OrderTotalsComponent, TextInputComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CarouselModule.forRoot(),
    PaginationModule.forRoot(),
    BsDropdownModule.forRoot()
  ],
  exports: [
    ReactiveFormsModule,
    PaginationModule,
    CarouselModule,
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    BsDropdownModule,
    TextInputComponent
  ]
})
export class SharedModule { }
