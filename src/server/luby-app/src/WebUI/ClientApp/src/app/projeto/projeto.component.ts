import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
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

  constructor(private client: ProjetoClient, private modalService: BsModalService) {
    client.getTodoItemsWithPagination(1, 10).subscribe(result => {
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
      },
      error => {
        let errors = JSON.parse(error.response);

        if (errors && errors.Title) {
          this.novoProjeto.error = errors.Title[0];
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
        },
        error => console.error(error)
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
      },
      error => console.error(error)
    );
  }
}
