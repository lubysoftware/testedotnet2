import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LancamentoHorasComponent } from './lancamento-horas.component';

describe('LancamentoHorasComponent', () => {
  let component: LancamentoHorasComponent;
  let fixture: ComponentFixture<LancamentoHorasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LancamentoHorasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LancamentoHorasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
