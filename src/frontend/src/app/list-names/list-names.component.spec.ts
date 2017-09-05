import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListNamesComponent } from './list-names.component';

describe('ListNamesComponent', () => {
  let component: ListNamesComponent;
  let fixture: ComponentFixture<ListNamesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListNamesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListNamesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
