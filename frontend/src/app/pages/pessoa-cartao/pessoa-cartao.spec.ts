import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PessoaCartao } from './pessoa-cartao';

describe('PessoaCartao', () => {
  let component: PessoaCartao;
  let fixture: ComponentFixture<PessoaCartao>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PessoaCartao]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PessoaCartao);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
