import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalificacionesEditarComponent } from './calificaciones-editar.component';

describe('CalificacionesEditarComponent', () => {
  let component: CalificacionesEditarComponent;
  let fixture: ComponentFixture<CalificacionesEditarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalificacionesEditarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalificacionesEditarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
