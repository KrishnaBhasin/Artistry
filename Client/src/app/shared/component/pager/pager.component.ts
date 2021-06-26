import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';


@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
  @Input() totalCount:number;
  @Input() pageSize:number;
  @Output() PageChange = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
    console.log("hello")
  }

  addPageChange(event:any)
  {
    this.PageChange.emit(event.page.toString());

  }


}
