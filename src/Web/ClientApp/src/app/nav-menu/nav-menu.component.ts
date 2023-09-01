import { Component } from '@angular/core';
import { RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public isExpanded: boolean = true;

  public collapse(): void {
    this.isExpanded = false;
  }

  public toggle(): void {
    this.isExpanded = !this.isExpanded;
  }
}
