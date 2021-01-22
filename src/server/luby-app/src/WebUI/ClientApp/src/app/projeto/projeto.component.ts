import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { NotificationService } from '../notification.service';
import { CreateProjetoCommand, PaginatedListOfProjetoDto, ProjetoClient, ProjetoDto, UpdateProjetoCommand } from '../web-api-client'; 
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-projeto',
  templateUrl: './projeto.component.html',
  styleUrls: ['./projeto.component.css']
})
export class ProjetoComponent {
  projetoVM: PaginatedListOfProjetoDto;
  projetoSelecionado: ProjetoDto;

  modalNovoProjeto: BsModalRef;
  modalEdicaoProjeto: BsModalRef;
  modalExclusaoProjeto: BsModalRef; 

  novoProjeto: any = {};
  itensPorPagina: number = 5;

  constructor(private client: ProjetoClient, private modalService: BsModalService, private notification: NotificationService) {
    client.getProjetosWithPagination(1, this.itensPorPagina).subscribe(result => {
      this.projetoVM = result;
    }, error => console.error(error));
  }

  onPageChange(pageNumber) { 
    this.client.getProjetosWithPagination(pageNumber, this.itensPorPagina).subscribe(result => {
      this.projetoVM = result;
    }, error => console.error(error)); 
  }

  showModalCreate(template: TemplateRef<any>): void {
    this.modalNovoProjeto = this.modalService.show(template);
  }

  showModalUpdate(template: TemplateRef<any>, p: ProjetoDto) {
    this.projetoSelecionado = new ProjetoDto(p);
    this.modalEdicaoProjeto = this.modalService.show(template);
  }

  saveProjeto(): void {
    let proj = ProjetoDto.fromJS({
      id: 0,
      nome: this.novoProjeto.nome,
    });

    this.client.create(<CreateProjetoCommand>{ nome: this.novoProjeto.nome }).subscribe(
      result => {
        proj.id = result;
        this.projetoVM.items.push(proj);
        this.modalNovoProjeto.hide();
        this.novoProjeto = {};

        this.notification.showSuccess("Projeto salvo com sucesso!", '');
      },
      error => {
        let errors = JSON.parse(error.response);

        if (errors) {
          this.novoProjeto.nomeError = errors.errors.Nome ? errors.errors.Nome[0] : null; 
        }
      }
    );
  }

  updateProjeto() {
    this.client.update(this.projetoSelecionado.id, UpdateProjetoCommand.fromJS(this.projetoSelecionado))
      .subscribe(
        () => {
          this.modalEdicaoProjeto.hide();
          this.projetoVM.items = this.projetoVM.items.filter(t => t.id != this.projetoSelecionado.id);
          this.projetoVM.items.push(this.projetoSelecionado);
          this.projetoSelecionado = new ProjetoDto();
          this.notification.showSuccess("Projeto atualizado com sucesso!", '');
        },
        error => {
          let errors = JSON.parse(error.response);

          if (errors) {
            this.novoProjeto.nomeError = errors.errors.Nome ? errors.errors.Nome[0] : null;
          }
        }
      );
  }

  confirmDelete(template: TemplateRef<any>, p: ProjetoDto) {
    this.projetoSelecionado = p;
    this.modalExclusaoProjeto = this.modalService.show(template);
  }

  deleteConfirmed(): void {
    this.client.delete(this.projetoSelecionado.id).subscribe(
      () => {
        this.modalExclusaoProjeto.hide();
        this.projetoVM.items = this.projetoVM.items.filter(t => t.id != this.projetoSelecionado.id);

        this.notification.showSuccess("Projeto removido com sucesso!", '');
      },
      error => console.error(error)
    );
  }
}
