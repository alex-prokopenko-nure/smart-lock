import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    iconRegistry: MatIconRegistry,
    sanitizer: DomSanitizer
    ) { 
      iconRegistry.addSvgIcon(
        "padlock",
        sanitizer.bypassSecurityTrustResourceUrl("assets/img/padlock.svg")
      );
    }

  ngOnInit() {
  }

}
