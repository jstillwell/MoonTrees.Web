﻿import { Component, Input } from '@angular/core';
import { MdGridListModule, MdGridTile, MdSpinner } from '@angular/material';

@Component({
  selector: 'tree-table',
  styleUrls: ['./table.component.less'],
  templateUrl: './table.component.html'
})
export class TableComponent {
    @Input() trees: Array<any>;
    @Input() isLoading: boolean;
}
