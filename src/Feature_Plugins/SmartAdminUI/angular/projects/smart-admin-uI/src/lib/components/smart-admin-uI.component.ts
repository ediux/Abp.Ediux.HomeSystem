import { Component, OnInit } from '@angular/core';
import { SmartAdminUIService } from '../services/smart-admin-uI.service';

@Component({
  selector: 'lib-smart-admin-uI',
  template: ` <p>smart-admin-uI works!</p> `,
  styles: [],
})
export class SmartAdminUIComponent implements OnInit {
  constructor(private service: SmartAdminUIService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
