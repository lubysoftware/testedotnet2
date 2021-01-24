import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NotificationService } from '../notification.service';
import { CreateDesenvolvedorHoraCommand, LacamentoHorasClient, PaginatedListOfDesenvolvedorHoraDto } from '../web-api-client';

@Component({
  selector: 'app-lancamento-horas',
  templateUrl: './lancamento-horas.component.html',
  styleUrls: ['./lancamento-horas.component.css']
})
export class LancamentoHorasComponent {
  lacamentoHorasVM: PaginatedListOfDesenvolvedorHoraDto; 
  modalNovoLacamentoHoras: BsModalRef; 
  novoLancamento: any = {};

  constructor(private client: LacamentoHorasClient, private modalService: BsModalService, private notification: NotificationService) {
    this.client.getWithPagination(1, 10, null).subscribe(result => {
      this.lacamentoHorasVM = result;
    }, error => console.error(error));
  }

  showModalCreate(template: TemplateRef<any>): void {
    this.modalNovoLacamentoHoras = this.modalService.show(template);
  }


  onPageChange(pageNumber) {
    this.client.getWithPagination(pageNumber, 10, null).subscribe(result => {
      this.lacamentoHorasVM = result;
    }, error => console.error(error));
  }

  saveLancamento(): void { 
    this.client.create(<CreateDesenvolvedorHoraCommand>{ fim: this.novoLancamento.fim, inicio: this.novoLancamento.inicio }).subscribe(
      result => { 
        this.client.getWithPagination(1, 10, null).subscribe(result => {
          this.lacamentoHorasVM = result;
        }, error => console.error(error));

        this.modalNovoLacamentoHoras.hide();
        this.novoLancamento = {};
        this.notification.showSuccess("Horas lançadas com sucesso!", '');
      },
      error => {
        let errors = JSON.parse(error.response);

        if (errors) {
          this.novoLancamento.inicioError = errors.errors.Inicio ? errors.errors.Inicio[0] : null;
          this.novoLancamento.fimError = errors.errors.Fim ? errors.errors.Fim[0] : null;
        }
      }
    );
  }
}
