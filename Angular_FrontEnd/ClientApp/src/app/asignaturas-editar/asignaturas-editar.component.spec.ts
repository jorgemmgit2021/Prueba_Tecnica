import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignaturasEditarComponent } from './asignaturas-editar.component';

describe('AsignaturasEditarComponent', () => {
  let component: AsignaturasEditarComponent;
  let fixture: ComponentFixture<AsignaturasEditarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AsignaturasEditarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AsignaturasEditarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
