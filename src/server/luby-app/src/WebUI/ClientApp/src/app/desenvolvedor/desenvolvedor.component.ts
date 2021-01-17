import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PaginatedListOfDesenvolvedorDto, DesenvolvedorClient, DesenvolvedorDto, CreateDesenvolvedorCommand, UpdateDesenvolvedorCommand, ProjetoDto, ProjetoClient } from '../web-api-client';

@Component({
  selector: 'app-desenvolvedor',
  templateUrl: './desenvolvedor.component.html',
  styleUrls: ['./desenvolvedor.component.css']
})
export class DesenvolvedorComponent {
  desenvolvedorVM: PaginatedListOfDesenvolvedorDto;

  desenvolvedorSelecionado: DesenvolvedorDto;

  modalNovoDesenvolvedor: BsModalRef;
  modalEdicaoDesenvolvedor: BsModalRef;
  modalExclusaoDesenvolvedor: BsModalRef;

  novoDesenvolvedor: any = {};
  projetos: ProjetoDto[];
  projetoSelecionado: bigint;

  constructor(private client: DesenvolvedorClient, clientProjeto: ProjetoClient, private modalService: BsModalService) {
    client.getDesenvolvedorWithPagination(1, 10).subscribe(result => {
      this.desenvolvedorVM = result;
    }, error => console.error(error));

    clientProjeto.getAll().subscribe(result => {
      this.projetos = result;
    }, error => console.error(error));
  }

  showModalCreate(template: TemplateRef<any>): void {
    this.modalNovoDesenvolvedor = this.modalService.show(template);
  }

  showModalUpdate(template: TemplateRef<any>, d: DesenvolvedorDto) {
    this.desenvolvedorSelecionado = new DesenvolvedorDto(d);
    this.modalEdicaoDesenvolvedor = this.modalService.show(template);
  }

  saveDesenvolvedor(): void {
    console.log(this.projetoSelecionado);

    let dev = DesenvolvedorDto.fromJS({
      id: 0,
      nome: this.novoDesenvolvedor.nome,
      cpf: this.novoDesenvolvedor.cpf,
      senha: this.novoDesenvolvedor.senha,
      email: this.novoDesenvolvedor.email,
      projetoId: this.projetoSelecionado
    });

    this.client.create(<CreateDesenvolvedorCommand>{
      nome: this.novoDesenvolvedor.nome, senha: this.novoDesenvolvedor.senha, email: this.novoDesenvolvedor.email, cpf: this.novoDesenvolvedor.cpf, projetoId: this.projetoSelecionado }).subscribe(
      result => {
        dev.id = result;
        this.desenvolvedorVM.items.push(dev);
        this.modalNovoDesenvolvedor.hide();
        this.novoDesenvolvedor = {};
      },
      error => {
        let errors = JSON.parse(error.response);

        if (errors && errors.Title) {
          this.novoDesenvolvedor.error = errors.Title[0];
        }
      }
    );
  }

  updateDesenvolvedor() {
    this.client.update(this.desenvolvedorSelecionado.id, UpdateDesenvolvedorCommand.fromJS(this.desenvolvedorSelecionado))
      .subscribe(
        () => {
          this.modalEdicaoDesenvolvedor.hide();
          this.desenvolvedorVM.items = this.desenvolvedorVM.items.filter(t => t.id != this.desenvolvedorSelecionado.id);
          this.desenvolvedorVM.items.push(this.desenvolvedorSelecionado);
          this.desenvolvedorSelecionado = new DesenvolvedorDto();
        },
        error => console.error(error)
      );
  }

  confirmDelete(template: TemplateRef<any>, d: DesenvolvedorDto) {
    this.desenvolvedorSelecionado = d;
    this.modalExclusaoDesenvolvedor = this.modalService.show(template);
  }

  deleteConfirmed(): void {
    this.client.delete(this.desenvolvedorSelecionado.id).subscribe(
      () => {
        this.modalExclusaoDesenvolvedor.hide();
        this.desenvolvedorVM.items = this.desenvolvedorVM.items.filter(t => t.id != this.desenvolvedorSelecionado.id);
      },
      error => console.error(error)
    );
  }
}
