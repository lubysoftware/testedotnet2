import { Component, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NotificationService } from '../notification.service';
import { CreateProjetoCommand, PaginatedListOfProjetoDto, ProjetoClient, ProjetoDto, UpdateProjetoCommand } from '../web-api-client'; 


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

  constructor(private client: ProjetoClient, private modalService: BsModalService, private notification: NotificationService ) {
    this.getProjetosWithPagination(1);
  }

  getProjetosWithPagination(pageNumber): void {
    this.client.getProjetosWithPagination(pageNumber, this.itensPorPagina).subscribe(result => {
      this.projetoVM = result;
    }, error => console.error(error)); 
  }

  onPageChange(pageNumber) {
    this.getProjetosWithPagination(pageNumber);
  }

  showModalCreate(template: TemplateRef<any>): void {
    this.modalNovoProjeto = this.modalService.show(template);
  }

  showModalUpdate(template: TemplateRef<any>, p: ProjetoDto) {
    this.projetoSelecionado = new ProjetoDto(p);
    this.modalEdicaoProjeto = this.modalService.show(template);
  }

  saveProjeto(): void { 
    this.client.create(<CreateProjetoCommand>{ nome: this.novoProjeto.nome }).subscribe(
      result => {
        this.getProjetosWithPagination(1);
        this.modalNovoProjeto.hide();
        this.novoProjeto = {}; 
        this.notification.showSuccess("Projeto salvo com sucesso!", '');
      },
      error => {
        this.notification.showWarning("Por favor, verifique se as informações estão corretas e tente novamente. ", 'Informações Inválidas');
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
          this.notification.showWarning("Por favor, verifique se as informações estão corretas e tente novamente. ", 'Informações Inválidas');
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
        this.getProjetosWithPagination(1);
        this.notification.showSuccess("Projeto removido com sucesso!", '');
      },
      error => {
        this.notification.showWarning("Por favor, verifique se as informações estão corretas e tente novamente. ", 'Informações Inválidas');
        console.error(error);
      }
    );
  }
}
