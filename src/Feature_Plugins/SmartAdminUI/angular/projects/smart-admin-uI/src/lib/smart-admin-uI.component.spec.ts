import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SmartAdminUIComponent } from './smart-admin-uI.component';

describe('SmartAdminUIComponent', () => {
  let component: SmartAdminUIComponent;
  let fixture: ComponentFixture<SmartAdminUIComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SmartAdminUIComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmartAdminUIComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
