import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CiclosEditarComponent } from './ciclos-editar.component';

describe('CiclosEditarComponent', () => {
  let component: CiclosEditarComponent;
  let fixture: ComponentFixture<CiclosEditarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CiclosEditarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CiclosEditarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
