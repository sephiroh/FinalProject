﻿import { Component } from '@angular/core';

@Component({
    selector: 'app-modal',
    template: `<div  class="modal fade" tabindex="-1" [ngClass]="{'in': visibleAnimate}"
       [ngStyle]="{'display': visible ? 'block' : 'none', 'opacity': visibleAnimate ? 1 : 0, 'background': 'rgba(0, 0, 0, 0.6)' }">
    <div class="modal-dialog">
      <div class="modal-content" (click)="$event.stopPropagation();">
        <div class="modal-header">
          <ng-content select=".app-modal-header"></ng-content>
        </div>
        <div class="modal-body">
          <ng-content select=".app-modal-body"></ng-content>
        </div>
        <div class="modal-footer">
          <ng-content select=".app-modal-footer"></ng-content>
        </div>
      </div>
    </div>
  </div>
  `
})

export class AppModalComponent {
    public visible = false;
    private visibleAnimate = false;

    public show(): void {
        this.visible = true;
        setTimeout(() => this.visibleAnimate = true, 100);
    }

    public hide(): void {
        this.visibleAnimate = false;
        setTimeout(() => this.visible = false, 300);
    }
}
