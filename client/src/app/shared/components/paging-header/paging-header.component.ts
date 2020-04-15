import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent implements OnInit {

  @Input() totalCount: number = 0;
  @Input() pagingHeaderPart1: string = '';
  @Input() pagingHeaderPart2: string = '';

  constructor() { }

  ngOnInit(): void {
  }

}
