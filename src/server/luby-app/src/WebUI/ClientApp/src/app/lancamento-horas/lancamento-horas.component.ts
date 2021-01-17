import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CreateDesenvolvedorHoraCommand, DesenvolvedorHoraDto, LacamentoHorasClient, PaginatedListOfDesenvolvedorHoraDto } from '../web-api-client';

@Component({
  selector: 'app-lancamento-horas',
  templateUrl: './lancamento-horas.component.html',
  styleUrls: ['./lancamento-horas.component.css']
})
export class LancamentoHorasComponent {
  lacamentoHorasVM: PaginatedListOfDesenvolvedorHoraDto; 
  modalNovoLacamentoHoras: BsModalRef; 
  novoLancamento: any = {};

  constructor(private client: LacamentoHorasClient, private modalService: BsModalService) {
    client.getWithPagination(1, 10, null).subscribe(result => {
      this.lacamentoHorasVM = result;
    }, error => console.error(error));
  }

  showModalCreate(template: TemplateRef<any>): void {
    this.modalNovoLacamentoHoras = this.modalService.show(template);
  }

  saveLancamento(): void {
    console.log(this.novoLancamento);
    this.client.create(<CreateDesenvolvedorHoraCommand>{ fim: this.novoLancamento.fim, inicio: this.novoLancamento.inicio }).subscribe(
      result => { 
        this.client.getWithPagination(1, 10, null).subscribe(result => {
          this.lacamentoHorasVM = result;
        }, error => console.error(error));

        this.modalNovoLacamentoHoras.hide();
        this.novoLancamento = {};
      },
      error => {
        let errors = JSON.parse(error.response);

        if (errors && errors.Title) {
          this.novoLancamento.error = errors.Title[0];
        }
      }
    );
  }
}
